using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace SMS.Definitions.Classes
{
    public class Fees
    {
        public Guid FeeID { get; set; }
        public double FeeAmount { get; set; }
        public string FeeCode { get; set; }
        public DateTime Date { get; set; }
    }

    public class Student
    {
        public Guid StudentID { get; set; }
    }

    [Immutable]
    public class FeePayment
    {
        public Student StudentFee { get; set; }
        public Fees Fee { get; set; }
    }
}
