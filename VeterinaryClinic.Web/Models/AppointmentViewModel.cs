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
        /// Клиент, хозяин питомца
        /// </summary>
        public int PersonId { get; set; }
        public string PersonFullName { get; set; }
        public string Phone { get; set; }

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
        /// Какие были операции / вакцинации
        /// </summary>
        public string HaveOperations { get; set; }

        /// <summary>
        /// Аллергии
        /// </summary>
        public string Allergies { get; set; }

        /// <summary>
        /// Хронические заболевания
        /// </summary>
        public string ChronicDiseases { get; set; }

        /// <summary>
        /// Вид
        /// </summary>
        public int PetTypeId { get; set; }
        public string PetTypeName { get; set; } 

        /// <summary>
        /// Жалобы
        /// </summary>
        public string Complaints { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Дата последней обработки от паразитов
        /// </summary>
        public DateTime? ParasiteTreatmentDate { get; set; }

        /// <summary>
        /// Анамнез болезни
        /// </summary>
        public string Anamnesis { get; set; }

        /// <summary>
        /// Лечение
        /// </summary>
        public string Therapy { get; set; }

        /// <summary>
        /// Вакцинации / операции питомца
        /// </summary>
        public List<PetOperationViewModel> Operations { get; set; }
    }
}