using Speisekarte.Data.Classes;
using Speisekarte.Data.Enum;
using Speisekarte.Entities;
using Speisekarte.Models.Home;
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
            HomeModel model = new HomeModel();
            using(SQLContext context = GetSQLContext())
            {
                foreach(Menu menu in context.Menus)
                {
                    NonContextMenu NonMenu = new NonContextMenu
                    {
                        Name = menu.Name,
                        Meals = menu.Meals.ToList(),
                        Drinks = menu.Drinks.ToList(),
                        Cost = menu.Cost,
                        Description = menu.Description
                    };
                    model.Menus.Add(NonMenu);
                } 
            }

            return View(model);
        }

        public ActionResult Login()
        {
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