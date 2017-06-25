using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// Цена закупки
        /// </summary>
        public int PurchasePrice { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public int SellingPrice { get; set; }
    }
}