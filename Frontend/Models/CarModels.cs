using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models
{
    public class CarModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        private string _ImagePath;
        public string ImagePath
        {
            get;set;
        }
        public bool IsActive { get; set; }
        public bool IsHotPrice { get; set; }
    }
}