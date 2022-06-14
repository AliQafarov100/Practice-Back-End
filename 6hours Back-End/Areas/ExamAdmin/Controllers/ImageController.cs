using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using _6hours_Back_End.DAL;
using _6hours_Back_End.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace _6hours_Back_End.Areas.ExamAdmin.Controllers
{
    [Area("ExamAdmin")]
    public class ImageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ImageController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Image> images = await _context.Images.ToListAsync();
            return View(images);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Image image)
        {
            if (!ModelState.IsValid) return NotFound();
            if(image.Img != null)
            {
               
                if(image.Img.Length > 1024 * 1024)
                {
                    ModelState.AddModelError("Img", "File size mustn't more than 1Mb");
                    
                }
                string fileName = image.Img.FileName;
                string filePath = Path.Combine(_env.WebRootPath, "assets", "Image", "Cards");
                string fullPath = Path.Combine(filePath, fileName);

                using(FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    await image.Img.CopyToAsync(stream);
                }

                image.Photo = fileName;
                await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Img", "Please choose file");
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            Image image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
            if (image == null) return NotFound();

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        
        public async Task<IActionResult> Edit(int id,Image image)
        {
            if (!ModelState.IsValid) return View();
            Image existedImage = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
            

            if(image.Img != null)
            {
                if(image.Img.Length > 1024 * 1024)
                {
                    ModelState.AddModelError("Img", "File size mustn't more than 1Mb");
                }
                else
                {
                    string fileName = image.Img.FileName;
                    string filePath = Path.Combine(_env.WebRootPath, "assets", "Image", "Cards");
                    string fullPath = Path.Combine(filePath, fileName);

                    
                    using(FileStream stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await image.Img.CopyToAsync(stream);
                    }

                    string anyPath = _env.WebRootPath + @"assets\Image\Cards" + image.Photo;

                    if (System.IO.File.Exists(anyPath))
                    {
                        System.IO.File.Delete(anyPath);
                    }

                    image.Photo = fileName;
                }
                
            }
            else
            {
                ModelState.AddModelError("Img", "Please choose file");
                return View();
            }

            existedImage.Photo = image.Photo;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Image image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (image == null) return NotFound();

            return View(image);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteImage(int id)
        {
            if (!ModelState.IsValid) return View();
            Image image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

           _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            Image image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
            return View(image);
        }
    }
}
