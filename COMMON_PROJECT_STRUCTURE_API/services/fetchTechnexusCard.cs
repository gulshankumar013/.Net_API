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
        
    
        public async Task<responseData> FetchTechnexusCard(requestData req)

 {
            responseData resData = new responseData();
            resData.rData["rCode"] = 0;
            try
            {
                var list = new ArrayList();
                MySqlParameter[] myParams = new MySqlParameter[] {
                new MySqlParameter("@Id", req.addInfo["Id"]),
                };
                var sq = $"SELECT * FROM pc_student.giganexus_home_card WHERE id=@id;";
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
                    resData.rData["discription"] = data[0][0]["discription"];
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
