using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Domain.Models
{
    /// <summary>
    /// Процедура
    /// </summary>
    [Table("Operation", Schema = "dict")]
    public class Operation
    {
        public int OperationId { get; set; }
        public string OperationName { get; set; }

        /// <summary>
        /// Вид процедуры (операция, вакцинация, обследование)
        /// </summary>
        public int OperationTypeId { get; set; }
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Расходы на материалы
        /// </summary>
        public int MaterialCosts { get; set; }

        /// <summary>
        /// Должности, которые могур проводить процедуру
        /// </summary>
        public List<OperationPosition> Positions { get; set; }

        /// <summary>
        /// Приемы питомцев, на которых была назначенна данная процедура
        /// </summary>
        public List<PetOperation> Appointments { get; set; }


    }
}
