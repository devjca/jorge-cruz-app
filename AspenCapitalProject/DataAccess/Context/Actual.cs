using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Context
{
    public partial class Actual
    {
        public int ActualId { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public DateTime DateDeposited { get; set; }
        public int ProjectionId { get; set; }

       // public virtual Projection Projection { get; set; }
    }
}
