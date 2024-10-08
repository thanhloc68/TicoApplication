﻿using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Product
    {
        public int? id { get; set; }
        public string? shortcutName { get; set; }
        public string? name { get; set; }
        public bool? status { get; set; }
    }
}
