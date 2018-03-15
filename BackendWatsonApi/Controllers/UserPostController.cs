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
        public async Task<IActionResult> GetImages([FromRoute] int id)
        {
            var images = await _context.UserPost.Include("Image").Where(u => u.User.UserId == id).ToListAsync();

            if (images == null)
            {
                return NotFound();
            }

            return Ok(images);
        } 

        // POST: api/UserPost
        [HttpPost] 
        [Route("SaveImage")]
        public async Task<IActionResult> SaveImage(IFormFile file)
        {            
            if (file.Length <= 0)
            {
                return BadRequest("The image or the post body is invalid");
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

            await _context.SaveChangesAsync();

            return Ok(img);            
        }

        // POST: api/UserPost
        [HttpPost]    
        [Route("SaveImageDetails")]
        public IActionResult SaveImageDetails([FromBody] UserPostDetail details)
        {
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

            userPost.DateAdded = DateTime.Now;
            userPost.ImageId = details.ImgId;

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

                userPost.Classifications.Add(wClass);
                _context.Add(wClass);
            }

            _context.Add(userPost);

            place.UserPosts.Add(userPost);

            userPost.Place = place;            

            _context.Add(place);

            _context.SaveChanges();

            return Ok(details);
        }


        // DELETE: api/UserPost/stringOfThePicName
        [HttpDelete("{name}")]
        public IActionResult DeleteUserImage(string name)
        {
            var predictionImages = Path.Combine(_hostingEnvironment.WebRootPath, "savedPredictionImages");
            var filePath = Path.Combine(predictionImages, name);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return Ok($"{name} deleted successfully");
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.ImageId == id);
        }
    }
}