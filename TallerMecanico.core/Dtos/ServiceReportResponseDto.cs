using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerMecanico.Core.Dtos
{
    public class ServiceReportResponseDto
    {
        public int TotalServices { get; set; }
        public IReadOnlyList<ServiceReportItemDto> Services { get; set; } = new List<ServiceReportItemDto>();
    }
}