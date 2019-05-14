using Speisekarte.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speisekarte.Models.Admin
{
    public class MenuModel
    {
        public MenuModel()
        {
            Appetizer = new List<string>();
            MainCourse = new List<string>();
            Dessert = new List<string>();
            Drinks = new List<string>();
            Menu = new Menu();
        }

        public Menu Menu{ get; set; }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public List<string> Appetizer { get; set; }
        public List<string> MainCourse { get; set; }
        public List<string> Dessert { get; set; }
        public List<string> Drinks { get; set; }
        public string CurrentAppetizer { get; set; }
        public string CurrentMainCourse { get; set; }
        public string CurrentDessert { get; set; }
        public string CurrentDrink { get; set; }
    }
}