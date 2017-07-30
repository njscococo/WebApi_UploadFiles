using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.IO; 

namespace WebApiTest.Controllers
{
    //https://github.com/damienbod/WebApiFileUpload
    public class FileUploadController : ApiController
    {
        private static readonly string FileLocation = "e:\\tools";

        [Route("files")]
        [HttpPost]
        [MultipartContentFilter]
        public async Task<FileResult> UploadFiles()
        {
            var streamProvider = new MultipartFormDataStreamProvider(FileLocation);
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            var result = new FileResult
            {
                FileNames = streamProvider.FileData.Select(c => c.LocalFileName),
                Names = streamProvider.FileData.Select(c => c.Headers.ContentDisposition.FileName),
                ContentTypes = streamProvider.FileData.Select(c => c.Headers.ContentType.MediaType),
                Description = streamProvider.FormData["description"],
                CreatedTimestamp = DateTime.Now,
                UpdatedTimestamp = DateTime.Now,
                DownloadLink = "we will tell you"

            };

            return result;
            //return new FileResult
            //{
            //    FileNames = streamProvider.FileData.Select(c => c.LocalFileName),
            //    Names = streamProvider.FileData.Select(c=> c.Headers.ContentDisposition.FileName),
            //    ContentTypes = streamProvider.FileData.Select(c=> c.Headers.ContentType.MediaType),
            //    Description = streamProvider.FormData["description"],
            //    CreatedTimestamp = DateTime.Now,
            //    UpdatedTimestamp = DateTime.Now,
            //    DownloadLink = "we will tell you"

            //};
        }
    }


}
