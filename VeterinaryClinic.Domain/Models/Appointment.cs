using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Приём у врача
    /// </summary>
    [Table("Appointment", Schema = "dbo")]
    public class Appointment
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
        public Pet Pet { get; set; }

        /// <summary>
        /// Жалобы
        /// </summary>
        public string Complaints { get; set; }

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
        /// Примечание
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Процедуры, назначенные питомцу
        /// </summary>
        public List<PetOperation> Operations { get; set; }

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
    }
}
