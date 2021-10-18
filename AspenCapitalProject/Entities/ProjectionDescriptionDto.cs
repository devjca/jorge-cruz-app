using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Entities
{
    
    public class ProjectionDescriptionDto
    {
        public string Description { get; set; }
        public double Projected { get; set; }
        public double Deposit { get; set; }
        public DateTime MonthYear { get; set; }
        public double Payment { get; set; }
        
    }

    
}
