using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class ProcurementViewModel
    {
        public int ProcurementId { get; set; }

        /// <summary>
        /// Товар
        /// </summary>
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        /// <summary>
        /// Дата закупки
        /// </summary>
        public DateTime ProcurementDate { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int ProcurementAmount { get; set; }

        /// <summary>
        /// Цена закупки
        /// </summary>
        public int ProcurementPrice{ get; set; }
    }
}