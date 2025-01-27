using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarhammerPaintCenter.Data;
using WarhammerPaintCenter.Models;
using WarhammerPaintCenter.Models.Entities;

namespace WarhammerPaintCenter.Controllers
{
    public class PaintControler : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public PaintControler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }




        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddPaintViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Pobierz ID zalogowanego użytkownika
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var paint = new Paint
                {
                    Name = viewModel.Name,
                    Type = viewModel.Type,
                    Own = viewModel.Own,
                    UserId = userId // Powiąż farbę z użytkownikiem
                };

                await dbContext.Paints.AddAsync(paint);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List", "PaintControler");
            }

            return View(viewModel);
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> List()
        {
            // Pobierz ID zalogowanego użytkownika
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Pobierz farby tylko dla tego użytkownika
            var paints = await dbContext.Paints
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return View(paints);
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var paint = await dbContext.Paints.FindAsync(id);
            if (paint == null)
            {
                return NotFound();
            }
            return View(paint);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(Paint viewModel)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var paint = await dbContext.Paints.FirstOrDefaultAsync(p => p.Id == viewModel.Id && p.UserId == userId);

            if (paint is not null)
            {
                paint.Name = viewModel.Name;
                paint.Type = viewModel.Type;
                paint.Own = viewModel.Own;

                await dbContext.SaveChangesAsync();
            }
            else
            {
                return Unauthorized(); // Nieautoryzowany dostęp
            }

            return RedirectToAction("List", "PaintControler");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(Paint viewModel)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var paint = await dbContext.Paints.FirstOrDefaultAsync(p => p.Id == viewModel.Id && p.UserId == userId);

            if (paint is not null)
            {
                dbContext.Paints.Remove(paint);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                return Unauthorized(); // Nieautoryzowany dostęp
            }

            return RedirectToAction("List", "PaintControler");
        }
    }
}