using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Пол
    /// </summary>
    [Table("Sex" , Schema = "dict")]
    public class Sex
    {
        public int SexId { get; set; }
        public string SexName { get; set; }

        public List<Pet> Pets { get; set; }

        public List<Person> Persons { get; set; }
    }
}
