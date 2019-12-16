using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.Car
{
    public class CarModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsHotPrice { get; set; }
    }
}