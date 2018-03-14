using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BackendWatsonApi.Models;
using BackendWatsonApi.Services;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BackendWatsonApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]
    public class PredictionController : Controller
    {
        private readonly IApplicationConfiguration _appConfig;
        private readonly ApplicationDbContext _context;
        private VisualRecognitionService _watson;
        private IHostingEnvironment _hostingEnvironment;

        public PredictionController(IApplicationConfiguration appSettings, ApplicationDbContext ctx, IHostingEnvironment hosting)
        {
            _hostingEnvironment = hosting;

            // set the app configuration
            _appConfig = appSettings;

            // Instantiate the db context
            _context = ctx;

            // create a Visual Recognition Service instance
            _watson = new VisualRecognitionService();

            // set the IBM Watson credentials
            _watson.SetCredential(_appConfig.WatsonApiKey.ToString());            
        }                

        // POST api/prediction
        [HttpPost]        
        public IActionResult Post(IFormFile file)
        {                    
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
    }
}
