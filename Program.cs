using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.Sqlite;
using Elsa.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;


var elsaConfiguration = configuration.GetSection("Elsa");

builder.Services.AddElsa(elsa => { 
elsa.UseEntityFrameworkPersistence(ef => ef.UseSqlite())
    .AddConsoleActivities( )
    .AddHttpActivities(elsaConfiguration.Bind)
    .AddQuartzTemporalActivities()
    .AddWorkflowsFrom<Program>()   
    ;

});

builder.Services.AddElsaApiEndpoints();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseHttpActivities();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoitns =>
{
    endpoitns.MapControllers();
    endpoitns.MapFallbackToPage("/_Host");
});

app.MapRazorPages();

app.Run();
