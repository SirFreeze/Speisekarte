using Speisekarte.Data.Enum;
using Speisekarte.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Speisekarte.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using(SQLContext context = GetSQLContext())
            {
                Meal meal = new Meal()
                {
                    ID = Guid.NewGuid(),
                    Cost = 2.0,
                    Name = "Pommes",
                    Type = MealType.MainCourse.ToString()
                };

                Drink drink = new Drink()
                {
                    ID = Guid.NewGuid(),
                    Name = "Cola",
                    Cost = 2.00
                };

                Menu menu = new Menu()
                {
                    ID = Guid.NewGuid(),
                    Cost = 10.00,
                    Name = "Test Menu",
                };

                menu.Drinks.Add(drink);
                menu.Meals.Add(meal);

                context.Menus.Add(menu);
                context.SaveChanges();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private SQLContext GetSQLContext()
        {
            SqlConnection connection = new SqlConnection($@"data source=localhost\SQLEXPRESS;initial catalog=MenuDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

            return new SQLContext(connection, true);
        }

    }
}