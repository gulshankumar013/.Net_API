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

        public async Task<responseData> TechnexusCard(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.technexus_card WHERE name=@name";
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
                    var sq = @"INSERT INTO pc_student.technexus_card(image, name, discribbe, price) 
                               VALUES (@image, @name, @discribbe, @price)";
                    MySqlParameter[] insertParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@image", rData.addInfo["image"]),
                        new MySqlParameter("@name", rData.addInfo["name"]),
                        new MySqlParameter("@discribbe", rData.addInfo["discribbe"]),
                        new MySqlParameter("@price", rData.addInfo["price"]),
                    };
                    var insertResult = ds.executeSQL(sq, insertParams);

                    resData.rData["rMessage"] = "card added Successful";
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
