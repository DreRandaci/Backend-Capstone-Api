﻿using System;
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
        public async Task<IActionResult> SaveImage(IFormFile file, UserPost imageDetails)
        {            
            if (file.Length <= 0)
            {
                return BadRequest(file);
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

            //*****FOR TESTING*****//
            var user = _context.User.Where(u => u.UserId == 1).SingleOrDefault();            

            _context.UserPost.Add(imageDetails);
            await _context.SaveChangesAsync();

            return Ok(file);            
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