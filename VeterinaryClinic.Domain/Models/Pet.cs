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
        public int PetId { get; set; }

        /// <summary>
        /// Вид питомца
        /// </summary>
        public int PetTypeId { get; set; }
        public PetType PetType { get; set; }

        public string PetName { get; set; }

        /// <summary>
        /// Хозяин питомца
        /// </summary>
        public int PersonId { get; set; }
        public Person Person { get; set; }

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
