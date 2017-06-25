using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    public class Product
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

        /// <summary>
        /// Закупки
        /// </summary>
        public List<Procurement> Procurements { get; set; }

        /// <summary>
        /// Продажи
        /// </summary>
        public List<Procurement> Sales { get; set; }
    }
}
