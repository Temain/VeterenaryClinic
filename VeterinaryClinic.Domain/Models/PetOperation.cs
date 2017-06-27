using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Процедура для питомца
    /// </summary>
    [Table("PetOperation", Schema = "dbo")]
    public class PetOperation
    {
        public PetOperation()
        {
            CreatedAt = DateTime.Now;
        }

        public int PetOperationId { get; set; }

        /// <summary>
        /// Конкретный прием питомца у врача
        /// </summary>
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        /// <summary>
        /// Процедура
        /// </summary>
        public int OperationId { get; set; }
        public Operation Operation { get; set; }

        /// <summary>
        /// Дата и время процедуры
        /// </summary>
        public DateTime? OperationDate { get; set; }

        /// <summary>
        /// Сотрудник, назначенный на выполнение процедуры (направление)
        /// </summary>
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

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
