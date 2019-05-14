using Speisekarte.Data.Classes;
using Speisekarte.Data.Enum;
using Speisekarte.Entities;
using Speisekarte.Models.Admin;
using Speisekarte.Models.Home;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                meal.ID = meal.ID == new Guid() ? Guid.NewGuid() : meal.ID;
                context.Meals.AddOrUpdate(meal);
                context.SaveChanges();
            }

            return RedirectToAction("MealView");
        }

        [HttpPost]
        public ActionResult AddDrinkToDb(Drink drink)
        {
            using (SQLContext context = GetSQLContext())
            {
                drink.ID = drink.ID == new Guid() ? Guid.NewGuid() : drink.ID;
                context.Drinks.AddOrUpdate(drink);
                context.SaveChanges();
            }

            return RedirectToAction("DrinkView");
        }

        [HttpPost]
        public ActionResult AddMenuToDb(string menuData)
        {
            MenuObject data = Newtonsoft.Json.JsonConvert.DeserializeObject<MenuObject>(menuData);

            using (SQLContext context = GetSQLContext())
            {
                Menu menu = new Menu
                {
                    ID = data.ID == new Guid() ? Guid.NewGuid() : data.ID,
                    Cost = data.Cost,
                    Name = data.Name,
                    Meals = context.Meals.Where(x => x.Name == data.MainCourse || x.Name == data.Dessert || x.Name == data.Appetizer).ToList(),
                    Drinks = context.Drinks.Where(x => x.Name == data.Drink).ToList(),
                    Description = data.Description
                };

                if(context.Menus.FirstOrDefault(x => x.ID == data.ID) != null)
                {
                    context.Menus.FirstOrDefault(x => x.ID == data.ID).Meals.Clear();
                    context.Menus.FirstOrDefault(x => x.ID == data.ID).Drinks.Clear();
                }

                context.Menus.AddOrUpdate(menu);
                context.SaveChanges();
            }


            return RedirectToAction("MenuView");
        }

        public ActionResult MealView()
        {
            MealModel model = new MealModel();
            using (SQLContext context = GetSQLContext())
            {
                model.Meals = context.Meals.ToList();
            }

            return View(model);
        }

        public ActionResult DrinkView()
        {
            DrinkModel model = new DrinkModel();
            using (SQLContext context = GetSQLContext())
            {
                model.Drinks = context.Drinks.ToList();
            }

            return View(model);
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

        public ActionResult EditMenu(Guid id)
        {
            Menu menu = new Menu();
            using (SQLContext context = GetSQLContext())
            {
                menu = context.Menus.FirstOrDefault(x => x.ID == id);
            }

            return View(menu);
        }

        public ActionResult EditMeal(Guid id)
        {
            Meal meal = new Meal();
            using (SQLContext context = GetSQLContext())
            {
                meal = context.Meals.FirstOrDefault(x => x.ID == id);
            }

            return View("AddMeal",meal);
        }

        public ActionResult EditDrink(Guid id)
        {
            Drink drink = new Drink();
            using (SQLContext context = GetSQLContext())
            {
                drink = context.Drinks.FirstOrDefault(x => x.ID == id);
            }

            return View("AddDrink", drink);
        }

        public ActionResult RemoveMenu(Guid id)
        {
            using (SQLContext context = GetSQLContext())
            {
                context.Menus.FirstOrDefault(x => x.ID == id).Drinks.Clear();
                context.Menus.FirstOrDefault(x => x.ID == id).Meals.Clear();
                context.Menus.Remove(context.Menus.FirstOrDefault(x => x.ID == id));
                context.SaveChanges();
            }

            return RedirectToAction("MenuView");
        }

        public ActionResult RemoveMeal(Guid id)
        {
            using (SQLContext context = GetSQLContext())
            {
                context.Meals.FirstOrDefault(x => x.ID == id).Menus.Clear();
                context.Meals.Remove(context.Meals.FirstOrDefault(x => x.ID == id));
                context.SaveChanges();
            }

            return RedirectToAction("MealView");
        }

        public ActionResult RemoveDrink(Guid id)
        {
            using (SQLContext context = GetSQLContext())
            {
                context.Drinks.FirstOrDefault(x => x.ID == id).Menus.Clear();
                context.Drinks.Remove(context.Drinks.FirstOrDefault(x => x.ID == id));
                context.SaveChanges();
            }

            return RedirectToAction("DrinkView");
        }

        private SQLContext GetSQLContext()
        {
            SqlConnection connection = new SqlConnection($@"data source=localhost\SQLEXPRESS;initial catalog=MenuDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

            return new SQLContext(connection, true);
        }
    }
}