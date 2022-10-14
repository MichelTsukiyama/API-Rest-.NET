using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business
{
    public interface IFileBusiness
    {
         public byte[] GetFile(string fileName);
         public Task<FileDetailsVO> SaveFileToDisk(IFormFile file);
         public Task<List<FileDetailsVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}