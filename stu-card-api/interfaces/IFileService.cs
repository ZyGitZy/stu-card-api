namespace stu_card_api.interfaces
{
    public interface IFileService
    {
        Task<int> PostAsync(IFormFile formFile, string buckName = "headportrait");

        Task<int> PostUrlAsync(string url, string buckName = "headportrait");

        Task<int> PostStreamAsync(Stream stream, string fileType, string fileName, string buckName = "headportrait");
    }
}
