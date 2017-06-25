using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class SaleViewModel
    {
        public int SaleId { get; set; }

        /// <summary>
        /// Дата продажи
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Товар
        /// </summary>
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        /// <summary>
        /// Сколько продано
        /// </summary>
        public int SaleCount { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public int SalePrice { get; set; }
    }
}