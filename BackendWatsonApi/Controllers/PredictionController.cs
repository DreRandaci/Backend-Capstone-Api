using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackendWatsonApi.Models;
using Microsoft.Extensions.Configuration;
using BackendWatsonApi.Services;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using Microsoft.AspNetCore.Http;

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

        public PredictionController(IApplicationConfiguration appSettings, ApplicationDbContext ctx)
        {
            // set the app configuration
            _appConfig = appSettings;

            // Instantiate the db context
            _context = ctx;

            // create a Visual Recognition Service instance
            _watson = new VisualRecognitionService();

            // set the IBM Watson credentials
            _watson.SetCredential(_appConfig.WatsonApiKey.ToString());            
        }

        // GET api/prediction?img=thing OR url=thing
        [HttpGet]
        public IActionResult Get(string img, string imgUrl)
        {
            var search = img != null ? img : imgUrl;

            if (img == null && imgUrl == null)
            {
                throw new Exception("Please try uploading again. If the problem persists try a different image file format or restart the application");
            }

            // send search to Watson
            var result = _watson.Classify(search);
            return Ok(result.Images[0].Classifiers[0].Classes.ToArray());
        }

        // GET api/prediction/{an int}
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]        
        public async Task<IActionResult> Post(IFormFile file)
        {
            var stream = file.OpenReadStream();

            var fileName = file.FileName;
            var name = new string[] { fileName };

            var picType = new string[] { file.ContentType };

            // send search to Watson
            //var result = _watson.Classify(file);

            var result = _watson.Classify(stream.ToString(), name, picType);
            return Ok(result.Images[0].Classifiers[0].Classes.ToArray());

            //return Ok(file);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }        
    }
}
