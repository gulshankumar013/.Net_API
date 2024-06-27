using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class offerOnBrands
    {
        dbServices ds = new dbServices();

          public async Task<responseData> OfferOnBrands(requestData req)
        {
            responseData resData = new responseData();
            try
            {
                

                MySqlParameter[] insertParams = new MySqlParameter[]
              {
                        
                         new MySqlParameter("@image", req.addInfo["image"]),
                         new MySqlParameter("@brand", req.addInfo["brand"]),
                         new MySqlParameter("@discount", req.addInfo["discount"]),
                       
              };
                var sq = @"insert into pc_student.giganexus_Top_brand(image,brand,discount) values(@image,@brand,@discount)";

                var insertResult = ds.executeSQL(sq, insertParams);
                if (insertResult[0].Count() == null)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "Failed to create card";
                }
                else
                {
                    resData.rData["rCode"] = 0;
                    resData.rData["rMessage"] = "card created successfully";

                }
            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }

         public async Task<responseData>FetchTopBrandProduct(string details)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"SELECT * FROM pc_student.giganexus_Top_brand  ";

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
                            brand = rowData[2],
                            discount = rowData[3]
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