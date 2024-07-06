using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class paymentService
    {
        private readonly string _key = "rzp_test_umG39ij57Swre8";
        private readonly string _secret = "pMDwFNTxNKOr0Gy42WOkxKy5";

        public async Task<responseData> CreateOrder(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                RazorpayClient client = new RazorpayClient(_key, _secret);

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", rData.addInfo["amount"]); // Amount in paise
                options.Add("currency", "INR");
                options.Add("receipt", rData.addInfo["receipt"]);

                Order order = client.Order.Create(options);
                resData.rData["orderId"] = order["id"].ToString();
                resData.rData["amount"] = order["amount"].ToString();
                resData.rData["currency"] = order["currency"].ToString();
                resData.rData["status"] = "Order created successfully";
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> CapturePayment(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                RazorpayClient client = new RazorpayClient(_key, _secret);

                Payment payment = client.Payment.Fetch((string)rData.addInfo["payment_id"]);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", payment["amount"]); // Amount in paise

                Payment capturedPayment = payment.Capture(options);
                resData.rData["paymentId"] = capturedPayment["id"].ToString();
                resData.rData["status"] = "Payment captured successfully";
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }
    }
}
