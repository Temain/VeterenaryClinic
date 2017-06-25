using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class OperationViewModel
    {
        public int OperationId { get; set; }
        public string OperationName { get; set; }
        public int OperationTypeId { get; set; }
        public string OperationTypeName { get; set; }

        public List<PositionViewModel> Positions { get; set; }
    }
}