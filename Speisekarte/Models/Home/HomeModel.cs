using Speisekarte.Data.Classes;
using Speisekarte.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speisekarte.Models.Home
{
    public class HomeModel
    {
        public HomeModel()
        {
            Menus = new List<NonContextMenu>();
        }
        public List<NonContextMenu> Menus { get; set; }
    }
}