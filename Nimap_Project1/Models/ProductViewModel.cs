using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nimap_Project1.Models
{
    public class ProductViewModel
    {
        public List<ProductModel> ListProduct { get; set; }
        public Pager pager { get; set; }
    }
}