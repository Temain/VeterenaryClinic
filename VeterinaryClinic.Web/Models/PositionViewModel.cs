using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinaryClinic.Web.Models
{
    public class PositionViewModel
    {
        public int OperationPositionId { get; set; }

        public int OperationId { get; set; }
        public string OperationName { get; set; }

        public int PositionId { get; set; }
        public string PositionName { get; set; }
    }
}