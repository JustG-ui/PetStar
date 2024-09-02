using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetStar.Data;
using PetStar.Models;

namespace PetStar.Controllers
{
    public class AnimalController : Controller
    {
        private readonly PetStarContext _context;

        public AnimalController(PetStarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Animais.OrderBy(cli => cli.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id", "Nome", "Idade","Especie", "CódigoAnimal")] Animal animal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(animal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível cadastrar o cliente");
            }
            return View(animal);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var animal = await _context.Animais.SingleOrDefaultAsync(i => i.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);

        }
        public bool AnimalExists(int? id)
        {
            return _context.Animais.Any(cli => cli.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, [Bind("Nome", "Idade", "Especie", "CódigoAnimal")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(animal);

        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var animal = await _context.Animais.SingleOrDefaultAsync(i => i.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var animal = await _context.Animais.SingleOrDefaultAsync(i => i.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            var animal = await _context.Animais.SingleOrDefaultAsync(i => i.Id == id);
            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
