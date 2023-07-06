using ClosedXML.Excel;
using Domain.DomainModels;
using Domain.DTO;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Diagnostics.Metrics;
using MimeKit;
using MailKit;
using Service;
using Enums;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IMovieService _movieService;
        private IWebHostEnvironment _hostEnvironment;
        private IEmailServiceOwn _emailSender;
        public TicketController(ITicketService ticketService, IMovieService movieService, IWebHostEnvironment hostEnvironment, IEmailServiceOwn emailSender)
        {
            _ticketService = ticketService;
            _movieService = movieService;
            _hostEnvironment = hostEnvironment;
            _emailSender = emailSender;

        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAll();

            return View(tickets);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            var viewModel = await _ticketService.GetAddViewModel();

            return View(viewModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(AddTicketDto ticketDto)
        {
            await _ticketService.Add(ticketDto);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid ticketId)
        {
            var viewModel = await _ticketService.GetEditViewModel(ticketId);

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<int> DeleteTicket(Guid ticketId)
        {
            return await _ticketService.Delete(ticketId);
        }

        [HttpGet]
        public async Task<IActionResult> FilterTickets(string date)
        {
            var tickets = await _ticketService.FilterTicketsByDate(date);
            return PartialView("_ListTickets",tickets);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ExportToExcel(int genreId)
        {
           

            var tickets = await _ticketService.GetTicketsByGenreId(genreId);

            string path = Path.Combine(_hostEnvironment.WebRootPath, "Tickets.xlsx");

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                var wbook = new XLWorkbook(fileStream);
                var ws = wbook.Worksheet(1);
                var counter = 2;

                foreach (var ticket in tickets)
                {
                    ws.Cell($"A{counter}").Value = ticket.Movie.Name;
                    ws.Cell($"B{counter}").Value = ticket.Price;
                    ws.Cell($"C{counter}").Value = ticket.SeatNumber;
                    ws.Cell($"D{counter}").Value = ticket.Date.ToString("dd.MM.yyyy");
                    ws.Cell($"E{counter}").Value = ticket.Time;
                    counter++;
                }

                using (var memoryStream = new MemoryStream())
                {
                    wbook.SaveAs(memoryStream);
                    var content = memoryStream.ToArray();
                    return File(content, "application/vnd.ms-excel");
                }
            }
        }

    }
}
