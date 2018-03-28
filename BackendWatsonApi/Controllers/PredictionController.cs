using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BackendWatsonApi.Services;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BackendWatsonApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]
    public class PredictionController : Controller
    {
        private readonly IApplicationConfiguration _appConfig;        
        private VisualRecognitionService _watson;        

        public PredictionController(IApplicationConfiguration appSettings)
        {            
            // set the app configuration
            _appConfig = appSettings;           

            // create a Visual Recognition Service instance
            _watson = new VisualRecognitionService();

            // set the IBM Watson credentials
            _watson.SetCredential(_appConfig.WatsonApiKey.ToString());            
        }              
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Thank you for using the Watson API! Navigate to https://github.com/DreRandaci/Watson-API to view the docs.");
        }

        // POST api/prediction/ClassifyGeneric
        [HttpPost]
        [Route("ClassifyGeneric")]
        public IActionResult ClassifyGeneric(IFormFile file)
        {
            if (file.Length <= 0 || file.Headers == null || file.ContentType != "multipart/form-data")
            {
                return BadRequest("File is invalid or missing");
            }

            //Extract the byte data from the iformfile
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(file.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)file.Length);
                     
            //Grab image content
            var fileName = file.FileName;
            var picType = file.ContentType;

            //Sends the image to watson for classification
            var result = _watson.Classify(CoverImageBytes, fileName, picType, null, null, null, 0, "en");

            return Ok(result.Images[0]._Classifiers[0].Classes.ToList());
        }

        // POST api/prediction/ClassifyGenericUrl?url=someurl/pic.jpg
        [HttpPost]
        [Route("ClassifyGenericUrl")]
        public IActionResult ClassifyGenericUrl(string url)
        {
            if (url == null || url == "")
            {
                return BadRequest("String is null or empty");
            }

            //Sends the url to watson for classification. Must be a valid URL that ends in either a .jpg or .png
            var result = _watson.Classify(url);

            return Ok(result.Images[0].Classifiers.ToList());
        }

        // POST api/prediction/DetectFaces
        [HttpPost]
        [Route("DetectFaces")]
        public IActionResult DetectFaces(IFormFile file)
        {
            if (file.Length <= 0 || file.Headers == null || file.ContentType != "multipart/form-data")
            {
                return BadRequest("File is invalid or missing");
            }

            //Extract the byte data from the iformfile
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(file.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)file.Length);

            //Grab image content
            var fileName = file.FileName;
            var picType = file.ContentType;


            //Sends the image to watson for classification
            var result = _watson.DetectFaces(CoverImageBytes, fileName, picType);

            return Ok(result.Images[0].Faces.ToList());
        }

        // POST api/prediction/DetectFacesUrl?url=someurl/pic.jpg
        [HttpPost]
        [Route("DetectFacesUrl")]
        public IActionResult DetectFacesUrl(string url)
        {
            if (url == null || url == "")
            {
                return BadRequest("String is null or empty");
            }

            //Sends the url to watson for classification
            var result = _watson.DetectFaces(url);

            return Ok(result.Images[0].Faces.ToList());
        }

    }
}
