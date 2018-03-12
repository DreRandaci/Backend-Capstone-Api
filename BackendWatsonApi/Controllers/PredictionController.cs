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
using System.IO;

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
        public IActionResult Get()
        {
            
            return Ok(_context.Image.ToList());
        }

        // GET api/prediction/{an int}
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/prediction
        [HttpPost]        
        public async Task<IActionResult> Post(IFormFile file)
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
            return Ok(result.Images[0]._Classifiers[0].Classes.ToArray());
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
