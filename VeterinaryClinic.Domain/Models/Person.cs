using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Физическое лицо
    /// </summary>
    [Table("Person", Schema = "dbo")]
    public class Person
    {
        public Person()
        {
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        // [Required]
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
        /// Дата создания записи
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Дата удаления записи
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Питомцы
        /// </summary>
        public List<Pet> Pets { get; set; }

        /// <summary>
        /// Ставки сотрудников
        /// </summary>
        public List<Employee> Employees { get; set; }

        /// <summary>
        /// Метод возвращает фамилию имя отчество
        /// </summary>
        public string FullName
        {
            get
            {
                var fullName = "";
                if (!String.IsNullOrEmpty(LastName))
                {
                    fullName += LastName + " ";
                }

                fullName += FirstName;

                if (!String.IsNullOrEmpty(MiddleName))
                {
                    fullName += " " + MiddleName;
                }
                return fullName;
            }
        }

        /// <summary>
        /// Инициалы и фамилия: Иванов И.И.
        /// </summary>
        public string ShortName
        {
            get
            {
                const string initialTerminator = ".";
                var shortName = "";

                if (!String.IsNullOrEmpty(LastName))
                {
                    shortName += LastName + " ";
                }

                shortName += FirstName[0] + initialTerminator;

                if (!String.IsNullOrEmpty(MiddleName))
                {
                    shortName += MiddleName[0] + initialTerminator;
                }

                return shortName;
            }
        }
    }
}
