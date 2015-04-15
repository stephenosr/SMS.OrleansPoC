using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Orleans;
using Orleans.Runtime.Host;
using BrightSword.SwissKnife;

using SMS.Definitions.Classes;

namespace SMS.WebAPI.Controllers
{
    public class FeesController : ApiController
    {
        [HttpPost]
        //[ActionName("FeeCredit")]
        public async Task<Guid> FeePayment(Guid StudenID, string FeeCode, double FeeAmount)
        {

            var feeGrain = IFeePaymentGrain.Interfaces.FeeManagerFactory.GetGrain(Guid.NewGuid());
            FeePayment FeePymnt = new FeePayment()
            {
                Fee = new Fees()
                {
                    FeeAmount = FeeAmount,
                    FeeCode = FeeCode,
                    FeeID = SequentialGuid.NewSequentialGuid(),
                    Date = DateTime.Now
                },

                StudentFee = new Student()
                {
                    StudentID = Guid.NewGuid()
                }
            };

            await feeGrain.FeePaymentStall(FeePymnt);
            return FeePymnt.Fee.FeeID;
        }

        public async Task<HttpResponseMessage> FetchFee(Guid FeeID)
        {
            var feeGrain = IFeePaymentGrain.Interfaces.FeeManagerFactory.GetGrain(0);
            FeePayment feeDetails = await feeGrain.FeeEnquiry(FeeID); //Not sure if this has to be the long or Guid - Steve
            return Request.CreateResponse(HttpStatusCode.OK,feeDetails);
        }

        [HttpPost]
        [ActionName("FeeTest")]
        public HttpResponseMessage FeeTest(Guid StudenID, string FeeCode, double FeeAmount)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "All workign");
        }
    }
}
