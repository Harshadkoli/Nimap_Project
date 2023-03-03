﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nimap_Project1.Models
{
    public class ProductModel
    {
        public int TotalRecords { get; set; }
        [Required(ErrorMessage = "Please Enter ProductName")]
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please Select Category")]

        public int Product_CatId { get; set; }
        public int id { get; set; }
        public List<Category> CategoryList = new List<Category>();
        public IEnumerable<Category> CatList = null;
       
        public string message { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}