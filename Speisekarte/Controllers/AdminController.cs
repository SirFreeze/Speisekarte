using Speisekarte.Data.Classes;
using Speisekarte.Data.Enum;
using Speisekarte.Entities;
using Speisekarte.Models.Admin;
using Speisekarte.Models.Home;
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
                model.Drinks = context.Drinks.Select(x => x.Name).ToList();
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

            return View("AddMeal");
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

            return View("AddDrink");
        }

        [HttpPost]
        public ActionResult AddMenuToDb(string menuData)
        {
            MenuObject data = Newtonsoft.Json.JsonConvert.DeserializeObject<MenuObject>(menuData);

            using (SQLContext context = GetSQLContext())
            {
                Menu menu = new Menu();
                menu.ID = Guid.NewGuid();
                menu.Cost = data.Cost;
                menu.Name = data.Name;
                menu.Meals = context.Meals.Where(x => x.Name == data.MainCourse || x.Name == data.Dessert || x.Name == data.Appetizer).ToList();
                menu.Drinks = context.Drinks.Where(x => x.Name == data.Drink).ToList();
                menu.Description = data.Description;

                context.Menus.Add(menu);
                context.SaveChanges();
            }


            return RedirectToAction("AddMenu");
        }

        public ActionResult MenuView()
        {
            HomeModel model = new HomeModel();
            using (SQLContext context = GetSQLContext())
            {
                foreach (Menu menu in context.Menus)
                {
                    NonContextMenu NonMenu = new NonContextMenu
                    {
                        Name = menu.Name,
                        Meals = menu.Meals.ToList(),
                        Drinks = menu.Drinks.ToList(),
                        Cost = menu.Cost,
                        Description = menu.Description,
                        ID = menu.ID
                    };
                    model.Menus.Add(NonMenu);
                }
            }

            return View(model);
        }

        private SQLContext GetSQLContext()
        {
            SqlConnection connection = new SqlConnection($@"data source=localhost\SQLEXPRESS;initial catalog=MenuDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

            return new SQLContext(connection, true);
        }
    }
}