using Speisekarte.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speisekarte.Models.Admin
{
    public class MealModel
    {
        public MealModel()
        {
            Meals = new List<Meal>();
        }
        public List<Meal> Meals { get; set; }
    }
}