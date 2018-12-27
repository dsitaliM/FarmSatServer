using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmSatServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FarmSatServer.Pages
{
    public class ImageGalleryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<SatelliteImage> SatelliteImages { get; set; }


        public ImageGalleryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            SatelliteImages = await _context.SatelliteImages.ToListAsync();
        }
    }
}