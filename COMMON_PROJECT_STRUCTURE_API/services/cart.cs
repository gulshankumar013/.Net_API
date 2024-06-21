using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using System.Collections;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class cart
    {
        dbServices ds = new dbServices();

        public async Task<responseData>Cart(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus_cart WHERE name=@name";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@name", rData.addInfo["name"])
                };
                var dbData = ds.executeSQL(query, myParam);
                
                if (dbData[0].Count() > 0)
                {
                    resData.rData["rMessage"] = "Card already added";
                }
                else
                {
                    var sq = @"INSERT INTO pc_student.giganexus_cart(image, name,price) 
                               VALUES (@image, @name, @price)";
                    MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@image", rData.addInfo["image"]),
                        new MySqlParameter("@name", rData.addInfo["name"]),
                        new MySqlParameter("@price", rData.addInfo["price"]),
                    };
                    var insertResult = ds.executeSQL(sq, insertParams);

                    resData.rData["rMessage"] = "Card added successfully";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData>DeleteCart(requestData rData)
        {

            responseData resData = new responseData();
           try
            {
                // Your delete query
                var query = @"DELETE FROM pc_student.giganexus_cat_card WHERE id = @Id;";

                // Your parameters
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@Id", rData.addInfo["id"])
                };

                // Condition to execute the delete query
                bool shouldExecuteDelete = true;

                if (shouldExecuteDelete)
                {
                    int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

                    if (rowsAffected > 0)
                    {
                        resData.rData["rMessage"] = "DELETE SUCCESSFULLY.";
                    }
                    else
                    {
                        resData.rData["rMessage"] = "No rows affected. Delete failed.";
                    }
                }
                else
                {
                    resData.rData["rMessage"] = "Condition not met. Delete query not executed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Exception occurred: " + ex.Message  + ex;
            }
            return resData;
        }

         public async Task<responseData>UpdateCart(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE pc_student.giganexus_cart
                              SET image = @image, name = @name, discription = @discription, price = @price
                              WHERE id = @id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"]),
                    new MySqlParameter("@image", rData.addInfo["image"]),
                    new MySqlParameter("@name", rData.addInfo["name"]),
                    new MySqlParameter("@price", rData.addInfo["price"]),
                    
                };

                int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

                if (rowsAffected > 0)
                {
                    resData.rData["rMessage"] = "Update successful.";
                }
                else
                {
                    resData.rData["rMessage"] = "No rows affected. Update failed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Exception occurred: " + ex.Message + ex;
            }
            return resData;
        }

           public async Task<responseData> FetchCart(requestData req)

 {
            responseData resData = new responseData();
            resData.rData["rCode"] = 0;
            try
            {
                var list = new ArrayList();
                MySqlParameter[] myParams = new MySqlParameter[] {
                new MySqlParameter("@Id", req.addInfo["Id"]),
                };
                var sq = $"SELECT * FROM pc_student.giganexus_cart WHERE id=@id;";
                var data = ds.ExecuteSQLName(sq, myParams);

                if (data == null || data[0].Count() == 0)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "No Card is present...";
                }
                else
                {

                    resData.rData["id"] = data[0][0]["id"];
                    
                    resData.rData["image"] = data[0][0]["image"];
                    resData.rData["name"] = data[0][0]["name"];
                    resData.rData["price"] = data[0][0]["price"];
                    
                }

            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = ex.Message;

            }
            return resData;
        }

    }
}
