using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class update
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Update(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Your update query
                var query = @"UPDATE pc_student.giganexus 
                           SET name = @Name, mobile = @Mobile, email = @Email,password = @Password, state = @State, pin = @Pin 
                           WHERE id = @Id;";

                // Your parameters
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@Id", rData.addInfo["id"]),
                    new MySqlParameter("@Name", rData.addInfo["name"]),
                     new MySqlParameter("@Email", rData.addInfo["email"]),
                     new MySqlParameter("@Password", rData.addInfo["password"]),
                    new MySqlParameter("@Mobile", rData.addInfo["mobile"]),
                    new MySqlParameter("@State", rData.addInfo["state"]),
                    new MySqlParameter("@Pin", rData.addInfo["pin"])
                };

                // Condition to execute the update query
                bool shouldExecuteUpdate = true;

                if (shouldExecuteUpdate)
                {
                    int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

                    if (rowsAffected > 0)
                    {
                        resData.rData["rMessage"] = "UPDATE SUCCESSFULLY.";
                    }
                    else
                    {
                        resData.rData["rMessage"] = "No rows affected. Update failed.";
                    }
                }
                else
                {
                    resData.rData["rMessage"] = "Condition not met. Update query not executed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData> Delete(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Your delete query
                var query = @"DELETE FROM pc_student.giganexus WHERE id = @Id;";

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
                resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
            }
            return resData;
        }
    }
}
