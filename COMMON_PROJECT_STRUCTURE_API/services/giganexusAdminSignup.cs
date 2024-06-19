using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using System.Collections;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class giganexusAdminSignup
    {
        dbServices ds = new dbServices();

        public async Task<responseData>GiganexusAdminSignup(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus_admin WHERE name=@name";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@name", rData.addInfo["name"])
                };
                var dbData = ds.executeSQL(query, myParam);
                
                if (dbData[0].Count() > 0)
                {
                    resData.rData["rMessage"] = "Admin already exists";
                }
                else
                {
                    var sq = @"INSERT INTO pc_student.giganexus_admin(name,email,password) 
                               VALUES (@name, @email, @password)";
                    MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@name", rData.addInfo["name"]),
                        new MySqlParameter("@email", rData.addInfo["email"]),
                        new MySqlParameter("@password", rData.addInfo["password"]),
                    };
                    var insertResult = ds.executeSQL(sq, insertParams);

                    resData.rData["rMessage"] = "Admin added successfully";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }

        public async Task<responseData>DeleteGiganexusAdmin(requestData rData)
        {

            responseData resData = new responseData();
           try
            {
                // Your delete query
                var query = @"DELETE FROM pc_student.giganexus_admin WHERE id = @Id;";

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

         public async Task<responseData>UpdateGiganexusAdmin(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE pc_student.giganexus_admin
                              SET name = @name, email = @email, password = @password
                              WHERE id = @id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"]),
                     new MySqlParameter("@name", rData.addInfo["name"]),
                    new MySqlParameter("@email", rData.addInfo["email"]),
                    new MySqlParameter("@password", rData.addInfo["password"]),
                    
                };

                int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

                if (rowsAffected > 0)
                {
                    resData.rData["rMessage"] = "Admin Update successful.";
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

           public async Task<responseData> FetchGiganexusAdmin(requestData req)

 {
            responseData resData = new responseData();
            resData.rData["rCode"] = 0;
            try
            {
                var list = new ArrayList();
                MySqlParameter[] myParams = new MySqlParameter[] {
                new MySqlParameter("@Id", req.addInfo["Id"]),
                };
                var sq = $"SELECT * FROM pc_student.giganexus_admin WHERE id=@id;";
                var data = ds.ExecuteSQLName(sq, myParams);

                if (data == null || data[0].Count() == 0)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "No Admin is present...";
                }
                else
                {

                    resData.rData["id"] = data[0][0]["id"];
                    resData.rData["name"] = data[0][0]["name"];
                    resData.rData["email"] = data[0][0]["email"];
                    resData.rData["password"] = data[0][0]["password"];
                    
                }

            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = ex.Message;

            }
            return resData;
        }

            public async Task<responseData> FetchAllAdmin(string details)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus_admin ORDER BY id DESC ";

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
                            password = rowData[3]
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
    }
}
