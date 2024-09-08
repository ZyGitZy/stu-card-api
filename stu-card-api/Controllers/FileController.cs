using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using stu_card_api.interfaces;
using System.Text;

namespace stu_card_api.Controllers
{
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        IFileService fileService;
        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpPost]
        public async Task<int> PostAsync([FromQuery] string buckName, IFormFile formFile) => await this.fileService.PostAsync(formFile, buckName);

        [HttpGet("url")]
        public async Task<int> PostUrlAsync(string buckName, string formFile) => await this.fileService.PostUrlAsync(formFile, buckName);

    }
}
