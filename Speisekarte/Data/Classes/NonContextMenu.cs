﻿using Speisekarte.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speisekarte.Data.Classes
{
    public class NonContextMenu
    {
        public Guid ID { get; set; }
        public string  Name { get; set; }
        public double Cost { get; set; }
        public List<Meal> Meals { get; set; }
        public List<Drink> Drinks { get; set; }
        public string Description { get; set; }
    }
}