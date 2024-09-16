using Microsoft.AspNetCore.Http;
using stu_card_api.Entitys;
using stu_card_api.interfaces;
using stu_card_entity_store.Store;
using System.Reactive;

namespace stu_card_api.Services
{
    public class FileService : IFileService
    {
        IMinioService minioService;
        IEntityStore<FileEntity> entityStore;
        //readonly string buckName = "headportrait";

        public  FileService(IMinioService minioService, IEntityStore<FileEntity> entityStore)
        {
            this.minioService = minioService;
            this.entityStore = entityStore;
        }

        public async Task<int> PostStreamAsync(Stream stream, string fileType, string fileName, string buckName = "headportrait")
        {
            var entity = new FileEntity
            {
                BuckName = buckName,
                FileName = fileName,
                FileSize = stream.Length,
                FileType = fileType,
            };

            stream.Position = 0;
            var fileUrl = await this.minioService.UploadFile(buckName, fileName, stream, fileType, true);
            entity.FileUrl = fileUrl;

            var result = await this.entityStore.CreateAsync(entity);

            return result.Id;
        }

        public async Task<int> PostUrlAsync(string url, string buckName = "headportrait")
        {
            string extension = Path.GetExtension(new Uri(url).AbsolutePath);
            var mimeTypes = Units.Units.GetFilePrefixByValue(extension);
 
            extension = DateTimeOffset.Now.ToUnixTimeMilliseconds() + extension;
            var result = await this.minioService.UploadFileUrl(buckName, extension, mimeTypes, url) ?? throw new Exception("文件上传失败");
            var entity = new FileEntity
            {
                BuckName = buckName,
                FileName = result.fileName,
                FileSize = result.fileSize,
                FileType = mimeTypes,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                FileUrl = result.url
            };

            var res = await this.entityStore.CreateAsync(entity);

            return res.Id;
        }

        public async Task<int> PostAsync(IFormFile formFile, string buckName = "headportrait")
        {
            var entity = new FileEntity
            {
                BuckName = buckName,
                FileName = formFile.FileName,
                FileSize = formFile.Length,
                CreateTime = DateTime.Now,
                FileType = formFile.ContentType,
            };

            MemoryStream stream = new();
            await formFile.CopyToAsync(stream);
            stream.Position = 0;
            var fileUrl = await this.minioService.UploadFile(buckName, formFile.FileName, stream, formFile.ContentType, true);
            entity.FileUrl = fileUrl;

            var result = await this.entityStore.CreateAsync(entity);

            return result.Id;
        }
    }
}
