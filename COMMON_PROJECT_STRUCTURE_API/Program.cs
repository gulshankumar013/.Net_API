
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
                if (rData.eventID == "1002") // delete
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
