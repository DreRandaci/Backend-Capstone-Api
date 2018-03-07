using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendWatsonApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public Array Get()
        {
            // create a Visual Recognition Service instance
            VisualRecognitionService _visualRecognition = new VisualRecognitionService();

            // set the credentials
            _visualRecognition.SetCredential("57a2800e51432df69ca26797c1853f320591b787");

            //  classify using an image url
            var result = _visualRecognition.Classify("https://itrekkers.com/blog/wp-content/uploads/2016/03/fish-1200x600-700x350.jpg");
            //JsonConvert.DeserializeObject(result);

            Console.WriteLine(result);

            //var results = new List<string>();
            //results.Add(result.ToString());

            return result.Images[0].Classifiers[0].Classes.ToArray();
            //return new String[] { "result" };
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
