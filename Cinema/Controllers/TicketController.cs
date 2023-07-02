using Domain.DomainModels;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Cinema.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IMovieService _movieService;
        public TicketController(ITicketService ticketService, IMovieService movieService)
        {
            _ticketService = ticketService;
            _movieService = movieService;
        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAll();

            return View(tickets);
        }

        public async Task<IActionResult> Add()
        {
            var viewModel = await _ticketService.GetAddViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTicketDto ticketDto)
        {
            await _ticketService.Add(ticketDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid ticketId)
        {
            var viewModel = await _ticketService.GetEditViewModel(ticketId);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTicketDto ticketDto)
        {
            await _ticketService.Update(ticketDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<Ticket> GetTicket(Guid ticketId)
        {
            return await _ticketService.GetTicketById(ticketId);
        }

        [HttpDelete]
        public async Task<int> DeleteTicket(Guid ticketId)
        {
            return await _ticketService.Delete(ticketId);
        }

    }
}
