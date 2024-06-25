
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Text.Json;
using COMMON_PROJECT_STRUCTURE_API.services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebHost.CreateDefaultBuilder(args)
    .ConfigureServices(s =>
    {
        IConfiguration appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        s.AddSingleton<login>();
        s.AddSingleton<signup>();
        s.AddSingleton<update>();
        s.AddSingleton<signin>();
        s.AddSingleton<forgot_password>();
        s.AddSingleton<technexusCard>();
        s.AddSingleton<fetchTechnexusCard>();
        s.AddSingleton<giganexusCatCard>();
        s.AddSingleton<fetchgiganexusCatCard>();
        s.AddSingleton<cart>();
        s.AddSingleton<giganexusWishlist>();
        s.AddSingleton<contactUs>();
        s.AddSingleton<giganexusAdminSignup>();
        s.AddSingleton<adminSignin>();
        s.AddSingleton<fetchAllMessage>();
        s.AddSingleton<trandingProduct>();


        s.AddAuthorization();
        s.AddControllers();
        s.AddCors();
        // s.AddAuthentication("SourceJWT").AddScheme<SourceJwtAuthenticationSchemeOptions, SourceJwtAuthenticationHandler>("SourceJWT", options =>
        // {
        //     options.SecretKey = appsettings["jwt_config:Key"].ToString();
        //     options.ValidIssuer = appsettings["jwt_config:Issuer"].ToString();
        //     options.ValidAudience = appsettings["jwt_config:Audience"].ToString();
        //     options.Subject = appsettings["jwt_config:Subject"].ToString();
        // });
    })
    .Configure(app =>
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(options =>
            options.WithOrigins("https://localhost:5002", "http://localhost:5001, http://localhost:5173")
            .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseRouting();
        app.UseStaticFiles();

        app.UseEndpoints(e =>
        {
            var login = e.ServiceProvider.GetRequiredService<login>();
            var signup = e.ServiceProvider.GetRequiredService<signup>();
            var update = e.ServiceProvider.GetRequiredService<update>();
            var signin = e.ServiceProvider.GetRequiredService<signin>();
            var forgot_password = e.ServiceProvider.GetRequiredService<forgot_password>();
            var technexusCard = e.ServiceProvider.GetRequiredService<technexusCard>();
            var fetchTechnexusCard = e.ServiceProvider.GetRequiredService<fetchTechnexusCard>();
            var giganexusCatCard = e.ServiceProvider.GetRequiredService<giganexusCatCard>();
            var fetchgiganexusCatCard = e.ServiceProvider.GetRequiredService<fetchgiganexusCatCard>();
            var cart = e.ServiceProvider.GetRequiredService<cart>();
            var giganexusWishlist = e.ServiceProvider.GetRequiredService<giganexusWishlist>();
            var contactUs = e.ServiceProvider.GetRequiredService<contactUs>();
            var giganexusAdminSignup = e.ServiceProvider.GetRequiredService<giganexusAdminSignup>();
            var adminSignin = e.ServiceProvider.GetRequiredService<adminSignin>();
            var fetchAllMessage = e.ServiceProvider.GetRequiredService<fetchAllMessage>();
            var trandingProduct = e.ServiceProvider.GetRequiredService<trandingProduct>();
            

            e.MapPost("login",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await login.Login(rData));
            });

            e.MapPost("signin",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await signin.Signin(rData));
            });

            e.MapPost("signup",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await signup.Signup(rData));
            });

            
            e.MapPost("fetchUser",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await signup.FetchUser(rData));
            });
            
            
            e.MapPost("fetchAllUser",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);

                if (rData.eventID == "1001") // getUserByEmail
                {
                    var result = await signup.FetchAllUser(body);
                    await http.Response.WriteAsJsonAsync(result);
                }
            });



              e.MapPost("forgot_password",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await forgot_password.Forgot_password(rData));
            });

            e.MapPost("update",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await update.Update(rData));
            });

            e.MapPost("delete",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // delete
                    await http.Response.WriteAsJsonAsync(await update.Delete(rData));
            });

             e.MapPost("technexusCard",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await technexusCard.TechnexusCard(rData));
            });


            _ = e.MapPost("deleteTechnexusCard",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await technexusCard.DeleteTechnexusCard(rData));
           });

             e.MapPost("updateTechnexusCard",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await technexusCard.UpdateTechnexusCard(rData));
            });

      
           e.MapPost("fetchTechnexusCard",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);

                if (rData.eventID == "1001") // getUserByEmail
                {
                    var result = await fetchTechnexusCard.FetchTechnexusCard(body);
                    await http.Response.WriteAsJsonAsync(result);
                }
            });


            e.MapPost("giganexusCatCard",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusCatCard.GiganexusCatCard(rData));
            });

             e.MapPost("deleteGiganexusCatCard",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusCatCard.DeleteGiganexusCatCard(rData));
            });

               e.MapPost("updateGiganexusCatCard",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusCatCard.UpdateGiganexusCatCard(rData));
            });

            e.MapPost("fetchgiganexusCatCard",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await fetchgiganexusCatCard.FetchgiganexusCatCard(rData));
            });

            e.MapPost("cart",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await cart.Cart(rData));
            });

            e.MapPost("updatecart",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await cart.UpdateCart(rData));
            });

            e.MapPost("deletecart",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await cart.DeleteCart(rData));
            });

            e.MapPost("fetchcart",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await cart.FetchCart(rData));
            });

            e.MapPost("giganexusWishlist",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusWishlist.GiganexusWishlist(rData));
            });

            e.MapPost("deleteGiganexusWishlist",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusWishlist.DeleteGiganexusWishlist(rData));
            });

            e.MapPost("updateGiganexusWishlist",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusWishlist.UpdateGiganexusWishlist(rData));
            });

            e.MapPost("fetchGiganexusWishlist",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusWishlist.FetchGiganexusWishlist(rData));
            });

            e.MapPost("contactUs",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await contactUs.ContactUs(rData));
            });

            
           e.MapPost("fetchAllMessage",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);

                if (rData.eventID == "1001") // getUserByEmail
                {
                    var result = await fetchAllMessage.FetchAllMessage(body);
                    await http.Response.WriteAsJsonAsync(result);
                }
            });

              
            e.MapPost("deleteMessageId",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await fetchAllMessage.DeleteMessageId(rData));
            });


            e.MapPost("giganexusAdmin",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusAdminSignup.GiganexusAdminSignup(rData));
            });

            e.MapPost("deleteGiganexusAdmin",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusAdminSignup.DeleteGiganexusAdmin(rData));
            });

            e.MapPost("updateGiganexusAdmin",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusAdminSignup.UpdateGiganexusAdmin(rData));
            });

            e.MapPost("fetchGiganexusAdmin",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await giganexusAdminSignup.FetchGiganexusAdmin(rData));
            });


            e.MapPost("fetchAllAdmin",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);

                if (rData.eventID == "1001") // getUserByEmail
                {
                    var result = await giganexusAdminSignup.FetchAllAdmin(body);
                    await http.Response.WriteAsJsonAsync(result);
                }
            });

            e.MapPost("adminSignin",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await adminSignin.AdminSignin(rData));
            });

            
            e.MapPost("trandingProduct",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);
                if (rData.eventID == "1001") // update
                    await http.Response.WriteAsJsonAsync(await trandingProduct.TrandingProduct(rData));
            });

            e.MapPost("fetchTrandingProduct",
            [AllowAnonymous] async (HttpContext http) =>
            {
                var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
                requestData rData = JsonSerializer.Deserialize<requestData>(body);

                if (rData.eventID == "1001") // getUserByEmail
                {
                    var result = await trandingProduct.FetchTrandingProduct(body);
                    await http.Response.WriteAsJsonAsync(result);
                }
            });



            



            IConfiguration appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            e.MapGet("/dbstring",
                async c =>
                {
                    dbServices dspoly = new();
                    await c.Response.WriteAsJsonAsync("{'mongoDatabase':" + appsettings["mongodb:connStr"] + "," + " " + "MYSQLDatabase" + " =>" + appsettings["db:connStrPrimary"]);
                });

            e.MapGet("/bing",
                async c => await c.Response.WriteAsJsonAsync("{'Name':'Anish','Age':'26','Project':'COMMON_PROJECT_STRUCTURE_API'}"));
        });
    });

builder.Build().Run();

public record requestData
{
    [Required]
    public string eventID { get; set; }
    [Required]
    public IDictionary<string, object> addInfo { get; set; }
}

public record responseData
{
    public responseData()
    {
        eventID = "";
        rStatus = 0;
        rData = new Dictionary<string, object>();
    }
    [Required]
    public int rStatus { get; set; } = 0;
    public string eventID { get; set; }
    public IDictionary<string, object> addInfo { get; set; }
    public IDictionary<string, object> rData { get; set; }
}
