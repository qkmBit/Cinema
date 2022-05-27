using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Cinema.Models;
using Cinema.ViewModels;
using System.Web.Mvc;
using System;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly CinemaContext context;

        public HomeController(CinemaContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            IndexModel model = new IndexModel(context);
            model.onGet();
            List<Film> films = model.Films;
            return View(films);
        }
        public IActionResult VPlayer(long id)
        {
            IndexModel model = new IndexModel(context);
            model.getFilm(id);
            Film film = model.Film;
            return PartialView("VPlayer", film);
        }
        public IActionResult FilmFullPage(long id)
        {
            IndexModel model = new IndexModel(context);
            model.getFilm(id);
            Film film = model.Film;
            return PartialView("FilmFullPage", film);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Buy(ticketJsonModel model)
        {
            if (ModelState.IsValid)
            {
                IndexModel indexModel = new IndexModel(context);
                bool bought = indexModel.buyTickets(User.Identity.Name, model.tickets);
                if (!bought)
                {
                    UserOrders userOrder = new UserOrders { UserId = indexModel.User.Id, OrderDate = DateTime.Now, Tickets = indexModel.Tickets };
                    context.UserOrders.Add(userOrder);
                    context.SaveChangesAsync();
                    return Json(new { result = "Билеты куплены", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    ModelState.AddModelError("", "Билет уже куплен");
                }
            }
            return Json(new { result = "Билеты уже куплены другим пользователем", JsonRequestBehavior.DenyGet });
        }
        public IActionResult SessionTicketList(long id)
        {
            IndexModel model = new IndexModel(context);
            model.getSession(id);
            Session session = model.Session;
            return PartialView("SessionTicketList", session);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
