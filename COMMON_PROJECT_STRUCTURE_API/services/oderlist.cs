using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class orderlist
    {
        dbServices ds = new dbServices();
        public async Task<responseData> Orderlist(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                
                MySqlParameter[] insertParams = new MySqlParameter[]
              {
                        new MySqlParameter("@user_id", req.addInfo["user_id"]),
                         new MySqlParameter("@image", req.addInfo["image"]),
                         new MySqlParameter("@product_name", req.addInfo["product_name"]),
                         new MySqlParameter("@discription", req.addInfo["discription"]),
                         new MySqlParameter("@price", req.addInfo["price"]),
                         new MySqlParameter("@quantity", req.addInfo["quantity"]),
                         new MySqlParameter("@shipping_address", req.addInfo["shipping_address"]),
                         new MySqlParameter("@paymentId", req.addInfo["paymentId"]),
                        
              };
                var sq = @"insert into pc_student.giganexus_orderlist(user_id,image,product_name,discription,price,quantity,shipping_address,paymentId) values(@user_id,@image,@product_name,@discription,@price,@quantity,@shipping_address,@paymentId)";

                var insertResult = ds.executeSQL(sq, insertParams);
                if (insertResult[0].Count() == null)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "Failed to receive oder ";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "Order receive successfully";

                }
            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }
    }
}   