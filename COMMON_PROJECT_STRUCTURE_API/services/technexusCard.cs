using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class technexusCard
    {
        dbServices ds = new dbServices();

          public async Task<responseData> TechnexusCard(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus_home_card WHERE name=@name";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@name", req.addInfo["name"])
                };
                var dbData = ds.executeSQL(query, myParam);

                if (dbData[0].Count > 0)
                {
                    resData.rData["rMessage"] = "Card already added";
                }
                else
                {
                    byte[] imageData = null;
                    if (req.addInfo.ContainsKey("image"))
                    {
                        var filePath = req.addInfo["image"].ToString();
                        imageData = File.ReadAllBytes(filePath);
                    }

                    var sq = @"INSERT INTO pc_student.giganexus_home_card(image, name, discription, price) 
                               VALUES (@image, @name, @discription, @price)";
                    MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@image", MySqlDbType.Blob) { Value = imageData },
                        new MySqlParameter("@name", req.addInfo["name"]),
                        new MySqlParameter("@discription", req.addInfo["discription"]),
                        new MySqlParameter("@price", req.addInfo["price"]),
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
        

        public async Task<responseData>DeleteTechnexusCard(requestData rData)
        {

            responseData resData = new responseData();
           try
            {
                // Your delete query
                var query = @"DELETE FROM pc_student.giganexus_home_card WHERE id = @Id;";

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

         public async Task<responseData> UpdateTechnexusCard(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE pc_student.giganexus_home_card
                              SET image = @image, name = @name, discription = @discription, price = @price
                              WHERE id = @id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@image", rData.addInfo["image"]),
                    new MySqlParameter("@name", rData.addInfo["name"]),
                    new MySqlParameter("@discription", rData.addInfo["discription"]),
                    new MySqlParameter("@price", rData.addInfo["price"]),
                    new MySqlParameter("@id", rData.addInfo["id"])
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

    }
}
