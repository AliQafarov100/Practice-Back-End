using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _6hours_Back_End.DAL;
using _6hours_Back_End.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _6hours_Back_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Image> images = await _context.Images.ToListAsync();
            return View(images);
        }
    }
}
