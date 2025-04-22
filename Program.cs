using Apotek.Data;
using Apotek.Services.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Apotek.Middlewares;
using Apotek.Services.KategoriBarang;
using Apotek.Services.Supplier;
using Apotek.Services.Satuan;
using Apotek.Services.BarangDisplay;
using Apotek.Services.BarangGudangService;

var builder = WebApplication.CreateBuilder(args);
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//SERVICE
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IKategoriBarangService, KategoriBarangService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISatuanService, SatuanService>();
builder.Services.AddScoped<IBarangDisplayService, BarangDisplayService>();
builder.Services.AddScoped<IBarangGudangService, BarangGudangService>();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:8000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

//AUTH
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"], //ini backend url
            ValidAudience = jwtSection["Audience"], //ini frontend url
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSection["Key"]))
        };
    });





builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
//SEEDER
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();

    // Jalankan seeder
    context.Database.Migrate();
    Apotek.Seeder.UserSeeder.SeedUser(context);
    Apotek.Seeder.KategoriBarangSeeder.SeedKategoriBarang(context);
    Apotek.Seeder.SupplierSeeder.SeedSupplierSeeder(context);
    Apotek.Seeder.SatuanSeeder.SeedSatuan(context);
    // Apotek.Seeder.BarangSeeder.Seed(context);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();


