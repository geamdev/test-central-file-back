using UserProyect.Interfaces;
using UserProyect.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserData, UserDataRepository>();
builder.Services.AddScoped<IProfileData, ProfileDataRepository>();
builder.Services.AddScoped<IContact, ContactRepository>();
builder.Services.AddScoped<IUserContact, UserContactRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurar cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowPolicy", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
