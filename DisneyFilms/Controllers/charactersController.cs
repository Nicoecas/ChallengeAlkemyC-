using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyFilms.Models;
using Microsoft.AspNetCore.Authorization;

namespace DisneyFilms.Controllers
{
    [Authorize]
    public class charactersController : Controller
    {
        private readonly PelisDisneyContext _context;

        public charactersController(PelisDisneyContext context)
        {
            _context = context;
        }

        // GET: characters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personajes.ToListAsync());
        }

        // GET: characters/name/5
        public async Task<IActionResult> name(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string nombre = id.Replace("_", " ");
            var personaje = await _context.Personajes
                .FirstOrDefaultAsync(m => m.Nombre == nombre);
            if (personaje == null)
            {
                return NotFound();
            }

            return View(personaje);
        }
        // GET: characters/age/1
        public async Task<IActionResult> age(string id)
        {
            if (id == null)
            {
                return NotFound();
                    
            }
            int valor = Int32.Parse(id);
            return View(await _context.Personajes.Where(x => x.Edad == valor).ToListAsync());

        }

        // GET: characters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Personaje personaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personaje);
        }

        // GET: characters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await _context.Personajes.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }
            return View(personaje);
        }

        // POST: characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromBody] Personaje personaje)
        {
            if (id != personaje.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonajeExists(personaje.Nombre))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(personaje);
        }

        // GET: characters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await _context.Personajes
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (personaje == null)
            {
                return NotFound();
            }

            return View(personaje);
        }

        // POST: characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var personaje = await _context.Personajes.FindAsync(id);
            _context.Personajes.Remove(personaje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonajeExists(string id)
        {
            return _context.Personajes.Any(e => e.Nombre == id);
        }
    
    }
}
