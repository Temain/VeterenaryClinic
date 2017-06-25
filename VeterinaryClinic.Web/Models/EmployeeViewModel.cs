
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

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
        /// Дата приёма на работу
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public int PositionId { get; set; }
        public string PositionName { get; set; }

        /// <summary>
        /// Допустимые операции / вакцинации для сотрудника
        /// </summary>
        // public List<OperationViewModel> Operations { get; set; }
    }
}