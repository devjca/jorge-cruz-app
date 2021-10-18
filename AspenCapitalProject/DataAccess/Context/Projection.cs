using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Context
{
    public partial class Projection
    {
        ////public Projection()
        ////{
        ////    Actuals = new HashSet<Actual>();
        ////}

        public int ProjectionId { get; set; }
        public DateTime? StartDate { get; set; }
        public string Xml { get; set; }

        //public virtual ICollection<Actual> Actuals { get; set; }
    }
}
