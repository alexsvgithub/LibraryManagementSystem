using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using WebApplication1.DbContexts;
using WebApplication1.Data.Implementation;
using WebApplication1.Data.Interface;
using WebApplication1.Services.Implementation;
using WebApplication1.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// -----------------------
// Database (MySQL)
// -----------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// -----------------------
// Repositories & Services
// -----------------------
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILoansRepository, LoansRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoansService, LoansService>();
builder.Services.AddScoped<IMemberService, MemberService>();

// -----------------------
// Controllers & JSON Options
// -----------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// -----------------------
// Swagger / OpenAPI
// -----------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -----------------------
// Middleware
// -----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();



//using Microsoft.EntityFrameworkCore;
//using System.Text.Json.Serialization;
//using Swashbuckle.AspNetCore.Annotations;
//using WebApplication1.DbContexts;
//using WebApplication1.Data.Implementation;
//using WebApplication1.Data.Interface;
//using WebApplication1.Services.Implementation;
//using WebApplication1.Services.Interface;

//var builder = WebApplication.CreateBuilder(args);

//// -----------------------
//// Database (MySQL)
//// -----------------------
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
//    )
//);

//// -----------------------
//// Repositories & Services
//// -----------------------
//builder.Services.AddScoped<IBookRepository, BookRepository>();
//builder.Services.AddScoped<ILoansRepository, LoansRepository>();
//builder.Services.AddScoped<IMemberRepository, MemberRepository>();
//builder.Services.AddScoped<IBookService, BookService>();
//builder.Services.AddScoped<ILoansService, LoansService>();
//builder.Services.AddScoped<IMemberService, MemberService>();

//// -----------------------
//// Controllers & JSON Options
//// -----------------------
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        // Prevent circular reference issues
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//    });

//// -----------------------
//// Swagger / OpenAPI
//// -----------------------
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//    {
//        Title = "Library API",
//        Version = "v1"
//    });

//    c.EnableAnnotations(); // Enable [SwaggerSchema] annotations
//});

//var app = builder.Build();

//// -----------------------
//// Middleware
//// -----------------------
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1");
//        c.RoutePrefix = "swagger";
//    });
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();

