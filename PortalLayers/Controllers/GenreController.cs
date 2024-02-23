
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using PortalLayers.BLL.DTO;
using PortalLayers.BLL.DTO.Interfaces;
using PortalLayers.BLL.Interfaces;
using PortalLayers.Models;


namespace PortalLayers.Controllers
{
    public class GenreController : Controller
    {
        private readonly IUserService userService;
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        IWebHostEnvironment _appEnvironment;
        public GenreController(IUserService us, ISongService ss, IGenreService gs, IWebHostEnvironment appEnvironment)
        {
            userService = us;
            songService = ss;
            genreService = gs;
            _appEnvironment = appEnvironment;
        }
        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View(await genreService.GetGenres());

        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                GenreDTO g = await genreService.GetGenre((int)id);
                return View(g);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreDTO g)
        {
            if (ModelState.IsValid)
            {
                await genreService.CreateGenre(g);
                return View("~/Views/Genre/Index.cshtml", await genreService.GetGenres());
            }
            return View(g);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                GenreDTO g = await genreService.GetGenre((int)id);
                return View(g);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Genres/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GenreDTO g)
        {
            if (ModelState.IsValid)
            {
                await genreService.UpdateGenre(g);
                return View("~/Views/Genre/Index.cshtml", await genreService.GetGenres());
            }
            return View(g);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return NotFound();
            }

            GenreDTO g = await genreService.GetGenre((int)id);
            return View(g);
            
        }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
    }
}

// POST: Genres/Delete/5
[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await genreService.DeleteGenre(id);
            return View("~/Views/Genre/Index.cshtml", await genreService.GetGenres());
        }

        
    }
}
