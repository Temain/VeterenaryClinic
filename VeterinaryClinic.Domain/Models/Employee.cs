using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    [Table("Employee", Schema = "dbo")]
    public class Employee
    {
        public int EmployeeId { get; set; }

        /// <summary>
        /// Физ. лицо
        /// </summary>
        public int PersonId { get; set; }
        public Person Person { get; set; }

        /// <summary>
        /// Дата приёма на работу
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public int PositionId { get; set; }
        public Position Position { get; set; }

        /// <summary>
        /// Процедуры, которые выполнил или выполнит данный сотрудник
        /// </summary>
        public List<PetOperation> Operations { get; set; }
    }
}
