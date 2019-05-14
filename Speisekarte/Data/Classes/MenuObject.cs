using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speisekarte.Data.Classes
{
    public class MenuObject
    {
        public string Name { get; set; }
        public float Cost { get; set; }
        public string Appetizer { get; set; }
        public string MainCourse { get; set; }
        public string Dessert { get; set; }
        public string Drink { get; set; }
        public string Description { get; set; }
    }
}