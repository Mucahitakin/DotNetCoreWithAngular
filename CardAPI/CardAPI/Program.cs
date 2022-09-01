using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Inject Dbcontext Sql server connection value
builder.Services.AddDbContext<CardDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("CardDbConnectionString")));


builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (option =>
    {
        option.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    }));
});//step 1 :Angular ile apinin localhost'larý anlaþtý.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");//step 2:Angular ile apinin localhost'larý anlaþtý
app.UseAuthorization();

app.MapControllers();

app.Run();
