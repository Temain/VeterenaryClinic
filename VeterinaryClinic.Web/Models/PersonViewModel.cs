using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models.Person
{
    public class PersonViewModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [StringLength(500)]
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [StringLength(500)]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [StringLength(500)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public int SexId { get; set; }
        public string SexName { get; set; }

        /// <summary>
        /// Питомцы
        /// </summary>
        public List<PetViewModel> Pets { get; set; }

        /// <summary>
        /// Дата последней записи на приём
        /// </summary>
        public string LastAppointmentDate { get; set; }

        /// <summary>
        /// Ставки сотрудников
        /// </summary>
        public List<EmployeeViewModel> Employees { get; set; }
    }
}