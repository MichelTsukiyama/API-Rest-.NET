using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business;
using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost]
        [Route("uploadFile")]
        [ProducesResponseType(200, Type = typeof(FileDetailsVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile(IFormFile file)
        {
            FileDetailsVO detail = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }

        [HttpPost]
        [Route("uploadMultipleFiles")]
        [ProducesResponseType(200, Type = typeof(List<FileDetailsVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyFiles(List<IFormFile> files)
        {
            List<FileDetailsVO> details = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }

        [HttpGet]
        [Route("downloadFile/{fileName}")]
        [ProducesResponseType(200, Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> DownloadOneFile(string fileName)
        {
            byte[] buffer = _fileBusiness.GetFile(fileName);

            if(buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".","")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
            return new ContentResult();
            // return File(buffer, "application/octet-stream", fileName); //Aqui retorna o arquivo sendo possível fazer downlaod do mesmo pela Url
        }
    }
}