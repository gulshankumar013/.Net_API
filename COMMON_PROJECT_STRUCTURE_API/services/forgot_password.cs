using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class forgot_password
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Forgot_password(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus WHERE email=@EMAIL AND password=@CURRENT_PASSWORD";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@EMAIL", rData.addInfo["email"]),
                    new MySqlParameter("@CURRENT_PASSWORD", rData.addInfo["currentPassword"])
                };
                var dbData = ds.executeSQL(query, myParam);
                
                if (dbData[0].Count() > 0)
                {
                    var updateQuery = @"UPDATE pc_student.giganexus SET password=@NEW_PASSWORD WHERE email=@EMAIL";
                    MySqlParameter[] updateParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@NEW_PASSWORD", rData.addInfo["newPassword"]),
                        new MySqlParameter("@EMAIL", rData.addInfo["email"])
                    };
                    var updateResult = ds.executeSQL(updateQuery, updateParams);
                    
                    resData.rData["rMessage"] = "Password Updated Successfully";
                }
                else
                {
                    resData.rData["rMessage"] = "Invalid Email id And Current Password";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }
    }
}
