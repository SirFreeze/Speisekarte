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
               
            }

            return View();
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