using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortalLayers.BLL.DTO;
using PortalLayers.BLL.DTO.Interfaces;
using PortalLayers.BLL.Interfaces;
using PortalLayers.Models;



namespace PortalLayers.Controllers
{
    public class SongController : Controller
    {
        private readonly IUserService userService;
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        IWebHostEnvironment _appEnvironment;
        public SongController(IUserService us, ISongService ss, IGenreService gs, IWebHostEnvironment appEnvironment)
        {
            userService = us;
            songService = ss;
            genreService = gs;
            _appEnvironment = appEnvironment;
        }

        // GET: Songs

  
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetString("Id");
            if (id != null)
            {
                var user = await userService.GetById(int.Parse(id));
                if (user != null && user.Login == "admin")
                {
                    ViewBag.IsAdmin = true;
                }
            }
            return View("Index", await songService.GetSongs());
        }
        public async Task<IActionResult> IndexAdmin()
        {
            var model = await songService.GetSongs();
            return View(model);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                SongDTO song = await songService.GetSong((int)id);
                return View(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }
		public bool SongExists(SongDTO song)
		{

			// Проверяем, есть ли song в базе данных
			return songService.GetSongs().Result.Where(m => m.name == song.name &&
				m.singer == song.singer).ToList().FirstOrDefault() != null;
		}


		// GET: Songs/Create
		public async Task<IActionResult> Create()
		{
			var genres = await genreService.GetGenres();
			var temp= new SelectList(genres, "Id", "name");

            ViewData["ListGenres"] = temp;
		
            return View();
        }

        // POST: Songs/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(SongDTO s, int UserId, int GenreId, IFormFile? uploadedPreview, IFormFile? uploadedHref)
        {
			if (ModelState.IsValid)
			{

				if (SongExists(s))
				{

					return View("~/Views/Song/Error.cshtml");
				}
				if (uploadedPreview != null)
				{
					string path = "/img/" + uploadedPreview.FileName;
					using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
					{
						await uploadedPreview.CopyToAsync(fileStream);
					}
					s.pic = path;

				}
				if (uploadedHref != null)
				{
					string path1 = "/mp3/" + uploadedHref.FileName;
					using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path1, FileMode.Create))
					{
						await uploadedHref.CopyToAsync(fileStream);
					}
					s.file = path1;
				}
			}
			var id = HttpContext.Session.GetString("Id");

			if (ModelState.IsValid)
            {
				
				s.genreId = GenreId;
               s.userId = UserId;
				s.dateTime = DateTime.Now;
				await songService.CreateSong(s);
               return View("~/Views/Song/Index.cshtml", await songService.GetSongs());
                
            }
            ViewBag.ListGenres = new SelectList(await genreService.GetGenres(), "Id", "name", s.genre);
            return View(s);
        }
     
        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
			var genres = await genreService.GetGenres();
			var temp = new SelectList(genres, "Id", "name");

			ViewData["ListGenres"] = temp;
			try { 
            if (id == null)
            {
                return NotFound();
            }

                var song = await songService.GetSong((int)id);
                return View(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Songs/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SongDTO song, int UserId, int GenreId, IFormFile? uploadedPreview, IFormFile? uploadedHref)
        {
			

            if (ModelState.IsValid)
            {
               		
                if (uploadedPreview != null)
                {
                    string path = "/img/" + uploadedPreview.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedPreview.CopyToAsync(fileStream);
                    }
                    song.pic = path;
                }
                if (uploadedHref != null)
                {
                    string path1 = "/mp3/" + uploadedHref.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path1, FileMode.Create))
                    {
                        await uploadedHref.CopyToAsync(fileStream);
                    }
                    song.file = path1;
                }
            
				song.genreId = GenreId;
				song.userId = UserId;
				await songService.UpdateSong(song);
                    return View("~/Views/Song/Index.cshtml", await songService.GetSongs());
                          
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try { 

            if (id == null)
            {
                return NotFound();
            }

                SongDTO s = await songService.GetSong((int)id);
                return View(s);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await songService.DeleteSong(id);
            return View("~/Views/Song/Index.cshtml", await songService.GetSongs());         }

     
    }
}
