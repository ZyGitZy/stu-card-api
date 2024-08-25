using Microsoft.AspNetCore.Http;
using stu_card_api.Entitys;
using stu_card_api.interfaces;
using stu_card_entity_store.Store;

namespace stu_card_api.Services
{
    public class FileService : IFileService
    {
        IMinioService minioService;
        IEntityStore<FileEntity> entityStore;
        readonly string buckName = "headportrait";

        static Dictionary<string, string> mimeTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".csv", "text/csv"}
    };
        public FileService(IMinioService minioService, IEntityStore<FileEntity> entityStore)
        {
            this.minioService = minioService;
            this.entityStore = entityStore;
        }

        public async Task<int> PostUrlAsync(string url)
        {
            string extension = Path.GetExtension(new Uri(url).AbsolutePath);
            string contextType = mimeTypes[extension];
            var result = await this.minioService.UploadFileUrl(buckName, extension, contextType, url) ?? throw new Exception("文件上传失败");
            var entity = new FileEntity
            {
                BuckName = buckName,
                FileName = result.ObjectName,
                FileSize = result.Size,
                FileType = contextType,
            };

            var res = await this.entityStore.CreateAsync(entity);

            return res.Id;
        }

        public async Task<int> PostAsync(IFormFile formFile)
        {
            var entity = new FileEntity
            {
                BuckName = buckName,
                FileName = formFile.FileName,
                FileSize = formFile.Length,
                FileType = formFile.ContentType,
            };

            MemoryStream stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            stream.Position = 0;
            var fileUrl = await this.minioService.UploadFile(buckName, formFile.FileName, stream, formFile.ContentType, true);
            entity.FileUrl = fileUrl;

            var result = await this.entityStore.CreateAsync(entity);

            return result.Id;
        }
    }
}
