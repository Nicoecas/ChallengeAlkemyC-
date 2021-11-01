using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DisneyFilms.Models;
using Microsoft.AspNetCore.Authorization;

namespace DisneyFilms.Controllers
{
    [Authorize]
    public class moviesController : Controller
    {
        private readonly PelisDisneyContext _context;

        public moviesController(PelisDisneyContext context)
        {
            _context = context;
        }

        // GET: movies
        public async Task<IActionResult> Index()
        {
            var pelisDisneyContext = _context.Films.Include(f => f.GeneroNNavigation);
            return View(await pelisDisneyContext.ToListAsync());
        }

        // GET: movies/name/Nombre
        public async Task<IActionResult> name(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string titulo= id.Replace("_", " ");
            var film = await _context.Films
                .Include(f => f.GeneroNNavigation)
                .FirstOrDefaultAsync(m => m.Titulo == titulo);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }
        // GET: movies/genre/GENERO
        public async Task<IActionResult> genre(string id)
        {
            return View(await _context.Films.Where(x => x.GeneroN == id).ToListAsync());
        }

        //GET: movies/order/asc o desc
        public async Task<IActionResult> order(string id)
        {
            if (id == "asc")
            {
                return View(await _context.Films.OrderBy(x => x.Titulo).ToListAsync());
            }
            if (id == "desc")
            {
                return View(await _context.Films.OrderByDescending(x => x.Titulo).ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        // GET: movies/Create
        public IActionResult Create()
        {
            ViewData["GeneroN"] = new SelectList(_context.Generos, "Nombre", "Nombre");
            return View();
        }

        // POST: movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroN"] = new SelectList(_context.Generos, "Nombre", "Nombre", film.GeneroN);
            return View(film);
        }

        // GET: movies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["GeneroN"] = new SelectList(_context.Generos, "Nombre", "Nombre", film.GeneroN);
            return View(film);
        }

        // POST: movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromBody] Film film)
        {
            if (id != film.Titulo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Titulo))
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
            ViewData["GeneroN"] = new SelectList(_context.Generos, "Nombre", "Nombre", film.GeneroN);
            return View(film);
        }

        // GET: movies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.GeneroNNavigation)
                .FirstOrDefaultAsync(m => m.Titulo == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var film = await _context.Films.FindAsync(id);
            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(string id)
        {
            return _context.Films.Any(e => e.Titulo == id);
        }
    
    }
}
