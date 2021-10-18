using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ProjectionResponseDto
    {
        public int Projection { get; set; }       
        public List<HeaderProjection> HeaderProjection { get; set; }
    }

    public class HeaderProjection
    { 
        public string MonthYear { get; set; }
        public string Activity { get; set; }
        public double Estimated { get; set; }
        public List<DetailProjection> DetailProjections { get; set; }

    }
    public class DetailProjection
    {
        public string Activity { get; set; }
        public double Estimated { get; set; }
        public double Actual { get; set; }
        public double EstimatedBalance { get; set; }
    }
}
