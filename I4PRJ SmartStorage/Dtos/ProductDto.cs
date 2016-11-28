﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using I4PRJ_SmartStorage.Models.Domain;

namespace I4PRJ_SmartStorage.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double PurchasePrice { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Stock> Stock { get; set; }

        public DateTime Updated { get; set; }

        public string ByUser { get; set; }

        public bool IsDeleted { get; set; }
    }
}