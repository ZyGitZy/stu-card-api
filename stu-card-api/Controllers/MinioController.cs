using Microsoft.AspNetCore.Mvc;
using stu_card_api.interfaces;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.AccessControl;

namespace stu_card_api.Controllers
{
    [Route("api/[controller]")]
    public class MinioController : ControllerBase
    {
        IMinioService minioService;
        IDataAcquisitionService dataAcquisitionService;
        public MinioController(IMinioService minioService, IDataAcquisitionService dataAcquisitionService)
        {
            this.minioService = minioService;
            this.dataAcquisitionService = dataAcquisitionService;
        }

        /// <summary>
        /// 根据捅名和filename获取下载链接
        /// </summary>
        /// <param name="buckName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("downLoad")]
        public async Task<IActionResult> DownLoad([Required] string buckName, [Required] string fileName)
        {
            var result = await this.minioService.GetDownLoadUrlAsync(buckName, fileName);
            return this.Ok(result);
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="buckName"></param>
        /// <param name="fileName"></param>
        /// <param name="isPublic"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([Required] string buckName, [Required] string fileName, bool isPublic)
        {
            this.Request.Headers.TryGetValue("Content-Type", out var contentType);

            MemoryStream stream = new();

            await this.Request.Body.CopyToAsync(stream);
            stream.Position = 0;

            var result = await this.minioService.UploadFile(buckName, fileName, stream, contentType.ToString(), isPublic);

            return Ok(result);
        }

        /// <summary>
        /// 随机头像（流）
        /// </summary>
        /// <returns></returns>
        [HttpGet("random/image")]
        public async Task<IActionResult> GetRandomImage()
        {
            var (stream, fileName) = await this.minioService.GetRandomImage();
            string contentType = "application/octet-stream";
            return File(stream, contentType, fileName);
        }

        /// <summary>
        /// 随机头像（url）
        /// </summary>
        /// <returns></returns>
        [HttpGet("random/image/url")]
        public async Task<IActionResult> GetRandomImageUrl()
        {
            var url = await this.minioService.GetRandomImageUrl();
            return this.Ok(url);
        }
    }
}
