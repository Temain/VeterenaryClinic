
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        /// <summary>
        /// Физ. лицо
        /// </summary>
        public int PersonId { get; set; }
        public string PersonFullName { get; set; }

        /// <summary>
        /// Дата приёма на работу
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public int PositionId { get; set; }
        public string PositionName { get; set; }
    }
}