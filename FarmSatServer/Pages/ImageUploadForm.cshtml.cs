using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FarmSatServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace FarmSatServer.Pages
{
    public class ImageUploadFormModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IConfiguration Configuration;
        private readonly IHostingEnvironment _environment;

        [BindProperty] public SatelliteImage SatelliteImage { get; set; }

        public ImageUploadFormModel(IConfiguration configuration, ApplicationDbContext context, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            // TODO: folder creation goes here.
        }

        public async Task<IActionResult> OnPostAsync(IFormCollection form)
        {
            try
            {
                var file = form.Files.FirstOrDefault();
                var uploadsDirectory = Path.Combine(_environment.WebRootPath, "Gallery");
                var uploadsPath = Path.Combine(uploadsDirectory, file?.FileName);

                await UploadImage(file);

                SatelliteImage.Url = $"{file?.FileName}";


                _context.SatelliteImages.Add(SatelliteImage);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Successfully Uploaded Image";

                return RedirectToPage("ImageGallery");
            }
            catch (Exception ex)
            {
                return RedirectToPage("Error");
            }
        }

        private async Task UploadImage(IFormFile file)
        {
            var uploadsDirectory = Path.Combine(_environment.WebRootPath, "Gallery");
            var uploadsPath = Path.Combine(uploadsDirectory, file.FileName);

            using (var fileStream = new FileStream(uploadsPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}