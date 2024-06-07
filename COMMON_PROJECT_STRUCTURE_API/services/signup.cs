using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class signup
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Signup(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus WHERE email=@email";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@email", rData.addInfo["email"])
                };
                var dbData = ds.executeSQL(query, myParam);
                
                  if (dbData[0].Count() > 0)
                {
                    resData.rData["rMessage"] = "Duplicate Credentials";
                }
                else
                {
                    var sq = @"INSERT INTO pc_student.giganexus (name, email, password, mobile, state, pin) 
                               VALUES (@name, @email, @password, @mobile, @state, @PIN)";
                    MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@NAME", rData.addInfo["name"]),
                        new MySqlParameter("@EMAIL", rData.addInfo["email"]),
                        new MySqlParameter("@PASSWORD", rData.addInfo["password"]),
                        new MySqlParameter("@mobile", rData.addInfo["mobile"]),
                        new MySqlParameter("@state", rData.addInfo["state"]),
                        new MySqlParameter("@pin", rData.addInfo["pin"])
                    };
                    var insertResult = ds.executeSQL(sq, insertParams);

                    resData.rData["rMessage"] = "Signup Successful";
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
