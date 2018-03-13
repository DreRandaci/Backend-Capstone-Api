using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendWatsonApi.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BackendWatsonApi.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]
    [Route("api/Image")]
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public ImageController(ApplicationDbContext context, IHostingEnvironment hosting)
        {
            _hostingEnvironment = hosting;
            _context = context;
        }

        // GET: api/Image
        [HttpGet]
        public IEnumerable<UserPost> GetImage()
        {
            return _context.Image;
        }

        // GET: api/Image/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = await _context.Image.SingleOrDefaultAsync(m => m.UserPostId == id);

            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }
  

        // POST: api/Image
        [HttpPost]
        public async Task<IActionResult> PostImage(IFormFile file)
        {
            //file.FileName = $"{DateTime.Now.ToString()}\\{file.FileName}";
            var predictionImages = Path.Combine(_hostingEnvironment.WebRootPath, "savedPredictionImages");
            if (file.Length > 0)
            {
                 
                var filePath = Path.Combine(predictionImages, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            //*****FOR TESTING*****//
            //var user = _context.User.Where(u => u.UserId == 1).SingleOrDefault();

            //var place = new Place()
            //{
            //    Address = address,
            //    Notes = notes
            //};

            //var classification = new WatsonClassification()
            //{
            //    ClassifierId = classifier,
            //    ClassifierName =
            //};

            //var image = new UserPost()
            //{
            //    User = user,
            //    Place = place,
            //    Classification = classification,
            //    ImageName = picName,
            //    DateAdded = time
            //};

            //_context.Image.Add(image);
            //await _context.SaveChangesAsync();

            return Ok(file);


            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}            

            //return CreatedAtAction("GetImage", new { id = image.ImageId }, image);
        }

        // DELETE: api/Image/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = await _context.Image.SingleOrDefaultAsync(m => m.UserPostId == id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Image.Remove(image);
            await _context.SaveChangesAsync();

            return Ok(image);
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.UserPostId == id);
        }
    }
}