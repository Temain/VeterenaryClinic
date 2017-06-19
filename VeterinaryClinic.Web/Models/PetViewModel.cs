using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class PetViewModel
    {
        public int PetId { get; set; }

        /// <summary>
        /// Вид питомца
        /// </summary>
        public int PetTypeId { get; set; }
        public string PetTypeName { get; set; }

        public string PetName { get; set; }

        /// <summary>
        /// Хозяин питомца
        /// </summary>
        public int PersonId { get; set; }
        public string PersonFullName { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public int SexId { get; set; }
        public string SexName { get; set; }

        /// <summary>
        /// Приёмы у врача
        /// </summary>
        public List<AppointmentViewModel> Appointments { get; set; }
    }
}