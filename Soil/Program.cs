using System;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.HttpOverrides;

//var hostbuilder = new HostBuilder()
//    .ConfigureAppConfiguration((hostContext, configBuilder) =>
//    { })
//    .ConfigureLogging((hostContext, configLogging) =>
//    { })
//    .ConfigureServices((hostContext, services) =>
//    {
//        Console.WriteLine("ITS WORKING OMGGGG");
//    });

//await hostbuilder.RunConsoleAsync();

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001); // to listen for incoming http connection on port 5001
    //options.ListenAnyIP(7001, configure => configure.UseHttps()); // to listen for incoming https connection on port 7001
});
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.WebHost.UseUrls("http://localhost:5003");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
    app.UseForwardedHeaders();
}
else
{
    app.UseForwardedHeaders();
}
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
app.Run();