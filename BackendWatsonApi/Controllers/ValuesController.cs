﻿using System;
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

namespace BackendWatsonApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IApplicationConfiguration _appConfig;

        public ValuesController(IApplicationConfiguration appSettings)
        {
            _appConfig = appSettings;
        }

        // GET api/values
        [HttpGet]
        public Array Get()
        {
            // create a Visual Recognition Service instance
            VisualRecognitionService _visualRecognition = new VisualRecognitionService();

            // set the credentials
            _visualRecognition.SetCredential(_appConfig.WatsonApiKey.ToString());

            //  classify using an image url for mock data
            var result = _visualRecognition.Classify("https://itrekkers.com/blog/wp-content/uploads/2016/03/fish-1200x600-700x350.jpg");

            return result.Images[0].Classifiers[0].Classes.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
