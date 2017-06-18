using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }

        /// <summary>
        /// Дата приёма
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Вес 
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Температура
        /// </summary>
        public int Temperature { get; set; }

        /// <summary>
        /// Принимаемый петомец
        /// </summary>
        public int PetId { get; set; }
        public string PetName { get; set; }

        /// <summary>
        /// Жалобы
        /// </summary>
        public string Complaints { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Comment { get; set; }
    }
}