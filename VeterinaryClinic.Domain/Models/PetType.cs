using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Вид питомца
    /// </summary>
    [Table("PetType", Schema = "dbo")]
    public class PetType
    {
        public int PetTypeId { get; set; }
        public string PetTypeName { get; set; }

        public List<Pet> Pets { get; set; }
    }
}
