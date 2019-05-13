using Speisekarte.Data.Enum;
using Speisekarte.Entities;
using Speisekarte.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Speisekarte.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpPost]
        public ActionResult Login()
        {
            //TODO
            return View("Adminpanel");
        }

        public ActionResult Adminpanel()
        {
            return View();
        }

        public ActionResult AddMeal()
        {
            return View();
        }

        public ActionResult AddDrink()
        {
            return View();
        }

        public ActionResult AddMenu()
        {
            MenuModel model = new MenuModel();

            using (SQLContext context = GetSQLContext())
            {
                model.Appetizer = context.Meals.Where(x => x.Type == MealType.Appetizer.ToString()).Select(x => x.Name).ToList();
                model.MainCourse = context.Meals.Where(x => x.Type == MealType.MainCourse.ToString()).Select(x => x.Name).ToList();
                model.Dessert = context.Meals.Where(x => x.Type == MealType.Dessert.ToString()).Select(x => x.Name).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMealToDb(Meal meal)
        {
            using (SQLContext context = GetSQLContext())
            {
                meal.ID = Guid.NewGuid();
                context.Meals.Add(meal);
                context.SaveChanges();
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult AddDrinkToDb(Drink drink)
        {
            using (SQLContext context = GetSQLContext())
            {
                drink.ID = Guid.NewGuid();
                context.Drinks.Add(drink);
                context.SaveChanges();
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult AddMenuToDb()
        {
            return View();
        }

        private SQLContext GetSQLContext()
        {
            SqlConnection connection = new SqlConnection($@"data source=localhost\SQLEXPRESS;initial catalog=MenuDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

            return new SQLContext(connection, true);
        }
    }
}