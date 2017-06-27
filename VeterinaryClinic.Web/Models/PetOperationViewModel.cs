using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class PetOperationViewModel
    {
        public int PetOperationId { get; set; }

        /// <summary>
        /// Конкретный прием питомца у врача
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Процедура
        /// </summary>
        public int OperationId { get; set; }
        public string OperationName { get; set; }
        public int OperationPrice { get; set; }
        public int OperationMaterialCosts { get; set; }

        public int OperationTypeId { get; set; }
        public string OperationTypeName { get; set; }

        /// <summary>
        /// Дата и время процедуры
        /// </summary>
        public DateTime? OperationDate { get; set; }

        /// <summary>
        /// Сотрудник, назначенный на выполнение процедуры (направление)
        /// </summary>
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }

        public List<EmployeeViewModel> Employees { get; set; }
    }
}