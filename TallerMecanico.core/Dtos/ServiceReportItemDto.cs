using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerMecanico.Core.Dtos
{
    public class ServiceReportItemDto
    {
        public DateTime? DateService { get; set; }
        public string? Description { get; set; }
        public string ClientName { get; set; } = null!;
    }
}