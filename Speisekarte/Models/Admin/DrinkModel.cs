using Speisekarte.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speisekarte.Models.Admin
{
    public class DrinkModel
    {
        public DrinkModel()
        {
            Drinks = new List<Drink>();
        }
        public List<Drink> Drinks{ get; set; }
    }
}