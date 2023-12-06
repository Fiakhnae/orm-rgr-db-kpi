using Npgsql;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using System.Globalization;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private static string message = "";
        string date = "";
        private static Database db = new Database();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddRestaurant() {
            ViewData["Message"] = message;
            message = "";
            return View("AddRestaurant");
        }

        public IActionResult GenerateRestaurant() {
            try {
                db.GenerateRestaurant();
                ViewData["Message"] = "Successfully generated";
                return View("Generate");
            }
            catch {
                return View("ErrorPage");
            }
        }

        public IActionResult GenerateTable()
        {
            try {
                db.GenerateTable();
                ViewData["Message"] = "Successfully generated";
                return View("Generate");
            }
            catch {
                return View("ErrorPage");
            }
        }

        public IActionResult OnAddRestaurant(Restaurant restaurant)
        {
            try {
                db.AddRestaurant(restaurant);
                message = "Successfuly added";
                return RedirectToAction("AddRestaurant");
            }
            catch {
                return View("ErrorPage");
            }
        }
        public IActionResult EditRestaurant() {
            ViewData["Message"] = message;
            message = "";
            return View("EditRestaurant");
        }
        public IActionResult OnEditRestaurant(int id, Restaurant restaurant)
        {
            try {
                db.EditRestaurant(id, restaurant);
                message = "Successfully edited";
                return RedirectToAction("EditRestaurant");
            }
            catch {
                return View("ErrorPage");
            }
        }
        public IActionResult AddTable()
        {
            ViewData["Message"] = message;
            message = "";
            return View("AddTable");
        }
        public IActionResult OnAddTable(Table table) {
            try {
                db.AddTable(table);
                message = "Successfully added";
                return RedirectToAction("AddTable");
            }
            catch {
                return View("ErrorPage");
            }
        }

        public IActionResult EditTable()
        {
            ViewData["Message"] = message;
            message = "";
            return View("EditTable");
        }
        public IActionResult OnEditTable(int id, Table table) {
            try {
                db.EditTable(id, table);
                message = "Successfully edited";
                return RedirectToAction("EditTable");
            }
            catch {
                return View("ErrorPage");
            }
        }
        public IActionResult AddTableReservation()
        {
            ViewData["Message"] = message;
            message = "";
            return View("AddTableReservation");
        }
        public IActionResult OnAddTableReservation(TableReservation reservation) {
            try {
                db.AddTableReservation(reservation);
                message = "Successfully added";
                return RedirectToAction("AddTableReservation");
            }
            catch {
                return View("ErrorPage");
            }
        }

        public IActionResult EditTableReservation()
        {
            ViewData["Message"] = message;
            message = "";
            return View("EditTableReservation");
        }
        public IActionResult OnEditTableReservation(int id, TableReservation reservation) {
            try {
                db.EditTableReservation(id, reservation);
                message = "Successfully edited";
                return RedirectToAction("EditTableReservation");
            }
            catch {
                return View("ErrorPage");
            }
        }

        public IActionResult DeleteTableReservation(int id)
        {
            ViewData["Message"] = message;
            message = "";
            return View("DeleteTableReservation");
        }
        public IActionResult OnDeleteTableReservation(int id) {
            try {
                db.DeleteTableReservation(id);
                message = "Successfully deleted";
                return RedirectToAction("DeleteTableReservation");
            }
            catch {
                return View("ErrorPage");
            }
        }
    }
}
