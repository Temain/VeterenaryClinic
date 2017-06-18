using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Должность, которая может проводить процедуру
    /// </summary>
    [Table("OperationPosition", Schema = "dbo")]
    public class OperationPosition
    {
        public int OperationPositionId { get; set; }

        /// <summary>
        /// Процедура (обследование, операция, вакцинация)
        /// </summary>
        public int OperationId { get; set; }
        public Operation Operation { get; set; }

        /// <summary>
        /// Должность, которая может проводить процедуру
        /// </summary>
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
