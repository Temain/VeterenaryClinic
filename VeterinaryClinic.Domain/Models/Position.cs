using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Должность сотрудника
    /// </summary>
    [Table("Position", Schema = "dict")]
    public class Position
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }

        /// <summary>
        /// Сотрудники с данной должностью
        /// </summary>
        public List<Employee> Employees { get; set; }

        /// <summary>
        /// Процедуры, которые может проводить данная должность
        /// </summary>
        public List<OperationPosition> Operations { get; set; }
    }
}
