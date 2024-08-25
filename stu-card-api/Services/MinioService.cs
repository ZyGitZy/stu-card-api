using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Response;
using Minio.Exceptions;
using stu_card_api.Entitys;
using stu_card_api.Extentions;
using stu_card_api.interfaces;
using stu_card_entity_store.Store;
using System.IO;

namespace stu_card_api.Services
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClient minioClient;
        private readonly IOptions<MinioOptions> options;
        private readonly IEntityStore<FileEntity> fileStore;
        public MinioService(IMinioClient minioClient, IOptions<MinioOptions> options, IEntityStore<FileEntity> entityStore)
        {
            this.minioClient = minioClient;
            this.options = options;
            this.fileStore = entityStore;
        }

        public async Task CreateBucketAsync(string bucketName)
        {
            try
            {
                var exists = await this.minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName)).ConfigureAwait(false);
                if (!exists)
                {
                    await this.minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> GetRandomImageUrl()
        {
            var fileEntity = await GetRandomFile();

            return await this.GetDownLoadUrlAsync(fileEntity.BuckName, fileEntity.FileName);
        }

        public async Task<(byte[] stream, string fileName)> GetRandomImage()
        {
            var fileEntity = await GetRandomFile();

            using MemoryStream memoryStream = new();
            var getObjectArgs = new GetObjectArgs()
                .WithBucket(fileEntity.BuckName)
                .WithObject(fileEntity.FileName)
                .WithCallbackStream(obj => obj.CopyTo(memoryStream));

            await this.minioClient.GetObjectAsync(getObjectArgs);

            return (memoryStream.ToArray(), fileEntity.FileName);
        }

        public async Task<string> GetDownLoadUrlAsync(string buckName, string objectName)
        {
            if (string.IsNullOrWhiteSpace(buckName) || string.IsNullOrWhiteSpace(objectName))
            {
                throw new Exception("buckName or objectName is null");
            }
            try
            {
                await this.minioClient.StatObjectAsync(new StatObjectArgs().WithBucket(buckName)
                    .WithObject(objectName));

                string url = await this.minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs().WithBucket(buckName).WithObject(objectName).WithExpiry(60 * 60 * 24));

                return url;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Set(string bucketName)
        {
            try
            {
                var policy = $"{{\"Version\":\"2012-10-17\",\"Statement\":[{{\"Effect\":\"Allow\",\"Principal\":{{\"AWS\":[\"*\"]}},\"Action\":[\"s3:ListBucket\",\"s3:GetBucketLocation\"],\"Resource\":[\"arn:aws:s3:::{bucketName}\"]}},{{\"Effect\":\"Allow\",\"Principal\":{{\"AWS\":[\"*\"]}},\"Action\":[\"s3:GetObject\"],\"Resource\":[\"arn:aws:s3:::{bucketName}/*\"]}}]}}";
                var setPolicy = new SetPolicyArgs().WithBucket(policy).WithPolicy(policy);
                await this.minioClient.SetPolicyAsync(setPolicy);
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred: " + e);
            }

        }

        public async Task<PutObjectResponse> UploadFileUrl(string bucketName, string fileName, string contextType, string url, bool isPublic = true)
        {
            if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(fileName))
            {
                throw new Exception("buckName or objectName is null");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("url is null");
            }

            try
            {
                await SetPolicyAsync(bucketName, isPublic);
                var putAge = new PutObjectArgs()
                    .WithBucket(bucketName)
                     .WithObject("island.jpg")
                     .WithContentType(contextType)
                    .WithFileName(url);
                return await this.minioClient.PutObjectAsync(putAge);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<string> UploadFile(string bucketName, string fileName, MemoryStream memoryStream, string contextType, bool isPublic = true)
        {

            if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(fileName))
            {
                throw new Exception("buckName or objectName is null");
            }

            if (memoryStream == null)
            {
                throw new Exception("未获取到文件流");
            }

            try
            {
                await SetPolicyAsync(bucketName, isPublic);
                var putAge = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName)
                    .WithStreamData(memoryStream)
                    .WithObjectSize(memoryStream.Length)
                    .WithContentType(contextType);
                await this.minioClient.PutObjectAsync(putAge);
                return $"{(options.Value.WithSSL ? "http://" : "https://")}{options.Value.Endpoint}/{bucketName}/{fileName}";
            }
            catch (Exception)
            {
                throw;
            }

        }

        private async Task SetPolicyAsync(string bucketName, bool isPublic)
        {
            await CreateBucketAsync(bucketName).ConfigureAwait(false);

            if (isPublic)
            {
                var policy = $"{{\"Version\":\"2012-10-17\",\"Statement\":[{{\"Effect\":\"Allow\",\"Principal\":{{\"AWS\":[\"*\"]}},\"Action\":[\"s3:ListBucket\",\"s3:GetBucketLocation\"],\"Resource\":[\"arn:aws:s3:::{bucketName}\"]}},{{\"Effect\":\"Allow\",\"Principal\":{{\"AWS\":[\"*\"]}},\"Action\":[\"s3:GetObject\"],\"Resource\":[\"arn:aws:s3:::{bucketName}/*\"]}}]}}";
                var setPolicy = new SetPolicyArgs().WithBucket(bucketName).WithPolicy(policy);
                await this.minioClient.SetPolicyAsync(setPolicy);
            }
        }

        private async Task<FileEntity> GetRandomFile()
        {
            var count = await this.fileStore.CountAsync();
            if (count == 0)
            {
                throw new Exception("没有任何图片");
            }

            var randomIndex = new Random().Next(0, count);

            var fileEntity = await this.fileStore.Query().Skip(randomIndex).Take(1).FirstOrDefaultAsync() ?? throw new Exception("没有任何图片");

            return fileEntity;
        }
    }
}
