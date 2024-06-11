
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class signin
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Signin(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus WHERE email=@EMAIL AND password=@PASSWORD";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@EMAIL", rData.addInfo["email"]),
                    new MySqlParameter("@PASSWORD", rData.addInfo["password"])
                };
                var dbData = ds.executeSQL(query, myParam);
                
                if (dbData[0].Count() > 0)
                {
                    resData.rData["rMessage"] = "Signin Successful";
                    // Add any additional data to response if needed, e.g., user details
                }
                else
                {
                    resData.rData["rMessage"] = "Invalid Email id And Password ";
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
