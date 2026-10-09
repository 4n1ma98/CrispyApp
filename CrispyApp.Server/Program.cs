using CrispyApp.Application;
using CrispyApp.Infrastructure;
using CrispyApp.Server; // For DataSeeder

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Clean Architecture Layers
builder.Services.AddApplicationServices();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Filename=crispyapp.db;Connection=Shared";
builder.Services.AddInfrastructure(connectionString);

var key = System.Text.Encoding.ASCII.GetBytes("ThisIsASecretKeyForCrispyApp12345!@#");
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Seed initial data for testing
app.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Allow debugging webassembly
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

// ---> Blazor Hosted Middlewares <---
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ---> Fallback to Blazor <---
app.MapFallbackToFile("index.html");

app.Run();
