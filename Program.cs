using DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AGREGAMOS LA CONEXION
builder.Services.AddDbContext<BarContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("BarConnection"));
});

var app = builder.Build();

///creamos un SCOPE PARA QUE SE EJECUTE Y CIERRE DE FORMA AUTOMATICA
/*
using (var scope = app.Services.CreateScope()) { 
    var context = scope.ServiceProvider.GetRequiredService<BarContext>();
    context.Database.Migrate();
}
*/

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();