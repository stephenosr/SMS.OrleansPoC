using Orleans;
using System.Threading.Tasks;
using System.Text;
using System;
using System.Collections.Generic;

using SMS.Definitions.Classes;

namespace SMS.IFeePaymentGrain.Interfaces
{
    /// <summary>
    /// Grain interface IGrain1
    /// </summary>
    public interface IFeePaymentGrain : IGrain
    {
        Task MakePayment(FeePayment payment);
    }
}
