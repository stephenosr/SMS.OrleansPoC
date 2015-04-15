using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SMS.Definitions.Classes;
using SMS.IFeePaymentGrain.Interfaces;
using Orleans.Concurrency;

namespace SMS.FeePaymentGrain.Class
{
    [StatelessWorker]
    [Reentrant]
    public class FeeManager : Grain, IFeeManager
    {
        public Task FeePaymentStall(FeePayment FeeEntry)
        {
            var feeGrain = FeePaymentGrainFactory.GetGrain(FeeEntry.Fee.FeeID);
            feeGrain.MakePayment(FeeEntry);
            return TaskDone.Done;
            //return Task.FromResult<long>(feeGrain.GetPrimaryKeyLong());
            //throw new NotImplementedException();
        }

        public Task<FeePayment> FeeEnquiry(Guid FeeID)
        {
            var feeGrain = FeePaymentGrainFactory.GetGrain(FeeID);
            return Task.FromResult<FeePayment>((FeePayment)feeGrain);
        }
    }
}
