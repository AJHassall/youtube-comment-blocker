using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<BlockListContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BlockedUserDb"))
    );



builder.Services.AddCors(options => {
    options.AddPolicy("policy", pb =>{
        //pb.AllowAnyOrigin();
        pb.WithOrigins("https://www.youtube.com");
        pb.AllowAnyHeader();
        pb.AllowAnyMethod();
        //pb.AllowCredentials();
    });
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BlockListContext>();  

    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();
app.UseCors("policy");
app.Run();
