using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using System.Collections;

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
                               VALUES (@name, @email, @password, @mobile, @state, @pin)";
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

         public async Task<responseData> FetchAllUser(string details)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus ORDER BY id DESC ";

                var dbData = ds.executeSQL(query, null);

                List<object> usersList = new List<object>();

                foreach (var rowSet in dbData)
                {
                    foreach (var row in rowSet)
                    {
                        List<string> rowData = new List<string>();

                        foreach (var column in row)
                        {
                            rowData.Add(column.ToString());
                        }

                        var user = new
                        {
                            id = rowData[0],
                            name = rowData[1],
                            email = rowData[2],
                            password = rowData[3],
                            mobile = rowData[4],
                            state = rowData[5],
                            pin = rowData[6],
                        };

                        usersList.Add(user);
                    }
                }

                resData.rData["users"] = usersList;
                resData.rData["rMessage"] = "Successful";
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
            }

            return resData;
        }

          public async Task<responseData>FetchUser(requestData req)

 {
            responseData resData = new responseData();
            resData.rData["rCode"] = 0;
            try
            {
                var list = new ArrayList();
                MySqlParameter[] myParams = new MySqlParameter[] {
                new MySqlParameter("@email", req.addInfo["email"]),
                };
                var sq = $"SELECT * FROM pc_student.giganexus WHERE email=@email;";
                var data = ds.ExecuteSQLName(sq, myParams);

                if (data == null || data[0].Count() == 0)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "No user is present...";
                }
                else
                {

                    resData.rData["id"] = data[0][0]["id"];
                    resData.rData["name"] = data[0][0]["name"];
                     resData.rData["email"] = data[0][0]["email"];
                    resData.rData["password"] = data[0][0]["password"];
                    resData.rData["mobile"] = data[0][0]["mobile"];
                    resData.rData["state"] = data[0][0]["state"];
                    resData.rData["pin"] = data[0][0]["pin"];
                    
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
