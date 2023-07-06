using Domain.DomainModels;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repo.Interfaces;
using Service.Implementations;
using Service.Interfaces;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private IGenreService _genreService;
        private IMovieService _movieService;
        public MoviesController(IGenreService genreService, IMovieService movieService)
        {
           _genreService = genreService;
            _movieService = movieService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var movies = await _movieService.GetAll();
            return View(movies);
        }

        public async Task<IActionResult> Add()
        {
            var genres = await _genreService.GetAll();
            ViewBag.Genres = genres.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieDto movieDto)
        {
             await _movieService.Add(movieDto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int movieId)
        {
            var genres = await _genreService.GetAll();
            ViewBag.Genres = genres.ToList();
            var movieFromDb = await _movieService.GetMovie(movieId);

            return View(movieFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMovieDto editMovieDto)
        {
            var genres = await _genreService.GetAll();
            ViewBag.Genres = genres.ToList();
            
            var movieFromDb = await _movieService.Update(editMovieDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<EditMovieDto> GetMovie(int movieId)
        {
            return await _movieService.GetMovie(movieId);

        }

        [HttpDelete]
        public async Task<int> DeleteMovie(int movieId)
        {
           return await _movieService.Delete(movieId);
        }

    }
}
