using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class fetchTechnexusCard
    {
        dbServices ds = new dbServices();
        
    
       public async Task<responseData> FetchTechnexusCard(string details)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus_home_card  ";

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
                            image = rowData[1],
                            name = rowData[2],
                            discription = rowData[3],
                            price = rowData[4],
                            brand = rowData[5],
                            about = rowData[6],
                            specifications = rowData[7]
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
