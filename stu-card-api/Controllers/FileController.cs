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
        public async Task<int> PostAsync(IFormFile formFile) => await this.fileService.PostAsync(formFile);

        [HttpPost("url")]
        public async Task<int> PostUrlAsync(string formFile) => await this.fileService.PostUrlAsync(formFile);

    }
}
