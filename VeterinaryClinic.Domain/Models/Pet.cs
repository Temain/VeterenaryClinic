using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Питомец
    /// </summary>
    [Table("Pet", Schema = "dbo")]
    public class Pet
    {
        public Pet()
        {
            CreatedAt = DateTime.Now;
        }

        public int PetId { get; set; }

        /// <summary>
        /// Вид питомца
        /// </summary>
        public int PetTypeId { get; set; }
        public PetType PetType { get; set; }

        public string PetName { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public int SexId { get; set; }
        public Sex Sex { get; set; }

        /// <summary>
        /// Хозяин питомца
        /// </summary>
        public int PersonId { get; set; }
        public Person Person { get; set; }

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
        /// Приёмы у врача
        /// </summary>
        public List<Appointment> Appointments { get; set; }

        /// <summary>
        /// Процедуры, которые были проведениы питомцу до приема
        /// </summary>
        // public List<PetOperation> Operations { get; set; }
    }
}
