using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;

using SMS.Definitions.Classes;
using SMS.IFeePaymentGrain.Interfaces;
using Orleans.Providers;
using Orleans.Concurrency;


namespace FeePaymentGrain
{
    public interface IFeePaymentGrainState : IGrainState
    {
         FeePayment FeeStoredValue { get; set; }
    }

    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    [StorageProvider(ProviderName = "AzureStore")]
    [Reentrant]
    public class FeePaymentGrain : Grain<IFeePaymentGrainState>, IFeePaymentGrain
    {
        private FeePayment _feePayment;

        public override Task OnActivateAsync()
        {
            var id = this.GetPrimaryKey();
            Console.WriteLine("Primary key ID - {0}",id);
            return base.OnActivateAsync();
        }

        
        public Task MakePayment(FeePayment payment)
        {
            try
            {
                Console.WriteLine("Your payment is {0}", payment);
                string _grainID = base.IdentityString;
                Console.WriteLine("The Grain ID is {0}",_grainID);
                _feePayment = payment;
                //this.State.FeeStoredValue = payment;
                Task.Run(() => SaveFee());
                //return Task.FromResult( string.Format("Payment made {0}",payment.ToString()));
                return TaskDone.Done;
            }
            catch
            {
                Console.WriteLine("Error");
                return TaskDone.Done;
            }
        }

        public Task FetchFee(Guid FeeID)
        {

            this.State.FeeStoredValue.Fee.FeeID = FeeID;
            return this.State.ReadStateAsync();
        }

        private async Task SaveFee()
        {
            Console.WriteLine("\n\nSaving fee...\n\n");
            this.State.FeeStoredValue = _feePayment;

            await this.State.WriteStateAsync();

            Console.WriteLine("\n\nSaved fee...\n\n");
        }


    }
}
