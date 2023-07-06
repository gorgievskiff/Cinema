using Domain.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        private IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
                _genreService = genreService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var genres = await _genreService.GetAll();
            return View(genres);
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(string genreName)
        {
            await _genreService.Add(new Genre { Name = genreName });
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetGenre(int genreId)
        {
            var genre = await _genreService.GetGenre(genreId);
            return Json(genre);
        }

        [HttpDelete]
        public async Task<int> DeleteGenre(int genreId)
        {
            return await _genreService.Delete(genreId);
        }

        [HttpPost]
        public async Task<IActionResult> EditGenre(int genreId,string genreName)
        {
            await _genreService.Update(genreId, genreName);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<List<Genre>> GetAll()
        {
            return await _genreService.GetAll();
        }

    }
}
