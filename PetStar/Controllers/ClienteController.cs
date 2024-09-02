using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetStar.Data;
using PetStar.Models;

namespace PetStar.Controllers
{
    public class ClienteController : Controller
    {
        private readonly PetStarContext _context;

        public ClienteController(PetStarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.OrderBy(cli => cli.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id", "Nome", "Email", "Telefone")] Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível cadastrar o cliente");
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.SingleOrDefaultAsync(i => i.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);

        }

        public bool ClientExists(long? id)
        {
            return _context.Clientes.Any(cli => cli.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long? id, [Bind("Id", "Nome", "Email", "Telefone")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(cliente.Id))
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
            return View(cliente);

        }
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.SingleOrDefaultAsync(i => i.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.SingleOrDefaultAsync(i => i.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            var cliente = await _context.Clientes.SingleOrDefaultAsync(i => i.Id == id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
