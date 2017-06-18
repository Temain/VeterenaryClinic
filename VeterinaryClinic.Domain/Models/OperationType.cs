using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Тип процедуры (вакцинация, операция, обследование)
    /// </summary>
    [Table("OperationType", Schema = "dict")]
    public class OperationType
    {
        public int OperationTypeId { get; set; }
        public string OperationName { get; set; }

        public List<Operation> Operations { get; set; }
    }
}
