using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    public class Procurement
    {
        public int ProcurementId { get; set; }

        /// <summary>
        /// Товар
        /// </summary>
        public int ProductId { get; set; }
        public Product Product { get; set; }

        /// <summary>
        /// Дата закупки
        /// </summary>
        public DateTime ProcurementDate { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int ProcurementAmount { get; set; }
    }
}
