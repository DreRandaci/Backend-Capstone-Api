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
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace BackendWatsonApi.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data", "application/javascript")]
    [Route("api/UserPost")]
    public class UserPostController : Controller
    {        
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public UserPostController(ApplicationDbContext context, IHostingEnvironment hosting)
        {
            _hostingEnvironment = hosting;
            _context = context;
        }        

        // GET: api/UserPost/5
        [HttpGet("{id}")]
        [Route("GetImages")]
        public IActionResult GetImages([FromRoute] int id)
        {            

            var predictionImages = Path.Combine(_hostingEnvironment.WebRootPath, "savedPredictionImages");

            var imgUris = _context
                .UserPost
                .Include(u => u.Classifications)
                .Include("Image")
                .Where(u => u.User.UserId == id).ToList();

            if (imgUris == null | imgUris.Count == 0)
            {
                return Ok("No pictures found");
            }

            return Ok(imgUris);
        } 

        // POST: api/UserPost
        [HttpPost] 
        [Route("SaveImage")]
        public async Task<IActionResult> SaveImage(IFormFile file)
        {            
            if (file.Length <= 0)
            {
                return BadRequest("The image is invalid");
            }

            var predictionImages = Path.Combine(_hostingEnvironment.WebRootPath, "savedPredictionImages");

            var filePath = Path.Combine(predictionImages, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var img = new Image()
            {
                ImageUri = filePath
            };

            _context.Add(img);                              

            _context.SaveChanges();

            return Ok(img);            
        }

        // POST: api/UserPost
        [HttpPost]    
        [Route("SaveImageDetails")]
        public IActionResult SaveImageDetails([FromBody] UserPostDetail details)
        {
            var user = _context.User
                .Where(u => u.UserId == details.UserId)
                .FirstOrDefault();            

            UserPost userPost = null;

            if (ModelState.IsValid)
            {
                userPost = new UserPost();
            }

            var place = new Place()
            {
                Address = details.Address,
                Notes = details.Notes
            };

            _context.Add(place);
            _context.SaveChanges();

            userPost.User = user;
            userPost.PlaceId = place.PlaceId;
            userPost.DateAdded = DateTime.Now;
            userPost.ImageId = details.ImgId;            
            _context.Add(userPost);
            _context.SaveChanges();

            foreach (var item in details.Classifications)
            {
                var wClass = new WatsonClassification()
                {
                    ClassifierId = item.ClassifierId,
                    ClassifierName = item.ClassifierName,
                    Class = item.Class,
                    ConfidenceScore = item.ConfidenceScore,
                    TypeHierarchy = item.TypeHierarchy ?? null
                };

                wClass.UserPostId = userPost.UserPostId;
                _context.Add(wClass);                
            }                      

            _context.SaveChanges();

            return Ok(details);
        }


        // DELETE: api/UserPost/stringOfThePicName
        [HttpDelete("{name}")]
        public IActionResult DeleteUserImage(int imgId)
        {
            var img = _context.Image
                .Where(i => i.ImageId == imgId)
                .SingleOrDefault();

            var predictionImages = Path.Combine(_hostingEnvironment.WebRootPath, "savedPredictionImages");
            var filePath = Path.Combine(predictionImages, img.ImageUri);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return Ok($"Image deleted successfully");
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.ImageId == id);
        }
    }
}