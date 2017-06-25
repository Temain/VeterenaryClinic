using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    public class Sale
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
        public Product Product { get; set; }

        /// <summary>
        /// Сколько продано
        /// </summary>
        public int SaleCount { get; set; } 
    }
}
