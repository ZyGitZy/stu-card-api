using Minio.DataModel.Response;

namespace stu_card_api.interfaces
{
    public interface IMinioService
    {
        Task<string> GetDownLoadUrlAsync(string buckName, string objectName);

        Task<string> UploadFile(string bucketName, string fileName, Stream memoryStream, string contextType, bool isPublic = true);

        Task<(string url,string fileName,long fileSize)?> UploadFileUrl(string bucketName,string fileName,string contextType, string url, bool isPublic = true);

        Task<(byte[] stream, string fileName)> GetRandomImage(string buckName);

        Task<string> GetRandomImageUrl(string buckName);
    }
}
