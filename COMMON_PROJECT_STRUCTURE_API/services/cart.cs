// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;
// using System;
// using System.Collections;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class cart
//     {
//         dbServices ds = new dbServices();

//         public async Task<responseData>Cart(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"SELECT * FROM pc_student.giganexus_cart WHERE name=@name";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@name", rData.addInfo["name"])
//                 };
//                 var dbData = ds.executeSQL(query, myParam);

//                 if (dbData[0].Count() > 0)
//                 {
//                     resData.rData["rMessage"] = "Card already added";
//                 }
//                 else
//                 {
//                     var sq = @"INSERT INTO pc_student.giganexus_cart(image, name,price) 
//                                VALUES (@image, @name, @price)";
//                     MySqlParameter[] insertParams = new MySqlParameter[]
//                     {
//                         new MySqlParameter("@image", rData.addInfo["image"]),
//                         new MySqlParameter("@name", rData.addInfo["name"]),
//                         new MySqlParameter("@price", rData.addInfo["price"]),
//                     };
//                     var insertResult = ds.executeSQL(sq, insertParams);

//                     resData.rData["rMessage"] = "Card added successfully";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "An error occurred: " + ex.Message;
//             }
//             return resData;
//         }

//         public async Task<responseData>DeleteCart(requestData rData)
//         {

//             responseData resData = new responseData();
//            try
//             {
//                 // Your delete query
//                 var query = @"DELETE FROM pc_student.giganexus_cat_card WHERE id = @Id;";

//                 // Your parameters
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@Id", rData.addInfo["id"])
//                 };

//                 // Condition to execute the delete query
//                 bool shouldExecuteDelete = true;

//                 if (shouldExecuteDelete)
//                 {
//                     int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

//                     if (rowsAffected > 0)
//                     {
//                         resData.rData["rMessage"] = "DELETE SUCCESSFULLY.";
//                     }
//                     else
//                     {
//                         resData.rData["rMessage"] = "No rows affected. Delete failed.";
//                     }
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "Condition not met. Delete query not executed.";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Exception occurred: " + ex.Message  + ex;
//             }
//             return resData;
//         }

//          public async Task<responseData>UpdateCart(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var query = @"UPDATE pc_student.giganexus_cart
//                               SET image = @image, name = @name, discription = @discription, price = @price
//                               WHERE id = @id";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@id", rData.addInfo["id"]),
//                     new MySqlParameter("@image", rData.addInfo["image"]),
//                     new MySqlParameter("@name", rData.addInfo["name"]),
//                     new MySqlParameter("@price", rData.addInfo["price"]),

//                 };

//                 int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

//                 if (rowsAffected > 0)
//                 {
//                     resData.rData["rMessage"] = "Update successful.";
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "No rows affected. Update failed.";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Exception occurred: " + ex.Message + ex;
//             }
//             return resData;
//         }

//            public async Task<responseData> FetchCart(requestData req)

//  {
//             responseData resData = new responseData();
//             resData.rData["rCode"] = 0;
//             try
//             {
//                 var list = new ArrayList();
//                 MySqlParameter[] myParams = new MySqlParameter[] {
//                 new MySqlParameter("@Id", req.addInfo["Id"]),
//                 };
//                 var sq = $"SELECT * FROM pc_student.giganexus_cart WHERE id=@id;";
//                 var data = ds.ExecuteSQLName(sq, myParams);

//                 if (data == null || data[0].Count() == 0)
//                 {
//                     resData.rData["rCode"] = 1;
//                     resData.rData["rMessage"] = "No Card is present...";
//                 }
//                 else
//                 {

//                     resData.rData["id"] = data[0][0]["id"];

//                     resData.rData["image"] = data[0][0]["image"];
//                     resData.rData["name"] = data[0][0]["name"];
//                     resData.rData["price"] = data[0][0]["price"];

//                 }

//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rCode"] = 1;
//                 resData.rData["rMessage"] = ex.Message;

//             }
//             return resData;
//         }

//     }
// }


// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;
// using System;
// using System.Collections;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class cart
//     {
//         dbServices ds = new dbServices();


//         public async Task<responseData> Cart(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 // Check if the cart entry already exists for the user
//                 var query = @"SELECT * FROM pc_student.giganexus_cart 
//                         WHERE user_id = @user_id 
//                           AND (trending_product_id = @trending_product_id )";

//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//             new MySqlParameter("@user_id", rData.addInfo["user_id"]),
//             new MySqlParameter("@trending_product_id", rData.addInfo["trending_product_id"]),
    
//                 };

//                 var dbData = ds.executeSQL(query, myParam);

//                 if (dbData[0].Count() > 0)
//                 {
//                     // Insert the cart entry if it doesn't exist
//                     var insertSq = @"INSERT INTO pc_student.giganexus_cart(user_id, trending_product_id)
//                              VALUES (@user_id, @trending_product_id)";
//                     MySqlParameter[] insertParams = new MySqlParameter[]
//                     {
//                 new MySqlParameter("@user_id", rData.addInfo["user_id"]),
//                 new MySqlParameter("@trending_product_id", rData.addInfo["trending_product_id"]),
                
//                     };

//                     var insertResult = ds.executeSQL(insertSq, insertParams);

//                     resData.rData["rMessage"] = "Card added successfully";
//                 }
//                 else
//                 {
//                       resData.rData["rMessage"] = "Card already added";
                   
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "An error occurred: " + ex.Message;
//             }
//             return resData;
//         }


using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
   public class cart
    {
        dbServices ds = new dbServices();

        public async Task<responseData> Cart(requestData rData)
        {
           responseData resData = new responseData();
            try
            {
                
                var sq = @"INSERT INTO pc_student.giganexus_cart(user_id, trending_product_id)
                                  VALUES (@user_id, @trending_product_id)";
                MySqlParameter[] insertParams = new MySqlParameter[]
                {
                    new MySqlParameter("@user_id", rData.addInfo["user_id"]),
                    new MySqlParameter("@trending_product_id", rData.addInfo["trending_product_id"]),
                };

                var insertResult = ds.executeSQL(sq, insertParams);

                resData.rData["rMessage"] = "Product added to cart successfully";
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }
        

        public async Task<responseData> DeleteCart(requestData rData)
        {
            responseData resData = new responseData();

            try
            {
                // Your delete query
                var query = @"DELETE FROM pc_student.giganexus_cart WHERE trending_product_id = @trending_product_id;";

                // Your parameters
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@trending_product_id", rData.addInfo["trending_product_id"])
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
    

        public async Task<responseData> UpdateCart(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var query = @"UPDATE pc_student.giganexus_cart
                              SET user_id = @user_id, trending_product_id = @trending_product_id, Home_card_product_id = @Home_card_product_id
                              WHERE id = @id";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@id", rData.addInfo["id"]),
                    new MySqlParameter("@user_id", rData.addInfo["user_id"]),
                    new MySqlParameter("@trending_product_id", rData.addInfo["trending_product_id"] ?? (object)DBNull.Value),
                    new MySqlParameter("@Home_card_product_id", rData.addInfo["Home_card_product_id"] ?? (object)DBNull.Value)
                };

                int rowsAffected = ds.ExecuteUpdateSQL(query, myParam);

                if (rowsAffected > 0)
                {
                    resData.rData["rMessage"] = "Update successful.";
                }
                else
                {
                    resData.rData["rMessage"] = "No rows affected. Update failed.";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Exception occurred: " + ex.Message;
            }
            return resData;
        }

       public async Task<responseData> FetchCart(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                MySqlParameter[] myParams = new MySqlParameter[] {
            new MySqlParameter("@user_id", rData.addInfo["user_id"]),
        };
                var sq = @"SELECT * FROM pc_student.giganexus_cart WHERE user_id=@user_id;";
                var data = ds.ExecuteSQLName(sq, myParams);

                if (data == null || data[0].Count() == 0)
                {
                    resData.rData["rCode"] = 1;
                    resData.rData["rMessage"] = "No cards found for the specified user.";
                }
                else
                {
                    foreach (var card in data[0])
                    {
                        if (card.ContainsKey("trending_product_id") && card["trending_product_id"] != null)
                        {
                            var trendingProductId = card["trending_product_id"];
                            if (trendingProductId != null && trendingProductId.ToString() != "{}")
                            {
                                MySqlParameter[] trendingParams = new MySqlParameter[] {
                            new MySqlParameter("@trending_product_id", trendingProductId)
                        };
                                var sq1 = @"SELECT * FROM pc_student.giganexus_trending_products WHERE id=@trending_product_id;";
                                var Data1 = ds.ExecuteSQLName(sq1, trendingParams);

                                if (Data1 != null && Data1[0].Count() > 0)
                                {
                                    card["trending_product_details"] = Data1[0][0];
                                }
                                else
                                {
                                    card["trending_product_details"] = "Trending product not found";
                                }
                            }
                        }
                    }
                    resData.rData["rCode"] = 0;
                    resData.rData["cards"] = data;
                }
            }
            catch (Exception ex)
            {
                resData.rData["rCode"] = 1;
                resData.rData["rMessage"] = "An error occurred: " + ex.Message;
            }
            return resData;
        }
    }
}


