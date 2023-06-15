using DMSTest.Back.Services;
using DMSTest.DTO.Common;
using DMSTest.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string MyCors = "MyCors";
var configuration = builder.Configuration;
var services = builder.Services;

services.AddScoped<DMSTest.DataAccess.Account>();
services.AddScoped<DMSTest.BAL.Account>();

services.AddScoped<IUserService, UserService>();

services.AddDataAccess(configuration);
services.AddControllers();

var appSettingSection = configuration.GetSection("AppSettings");
services.Configure<AppSettings>(appSettingSection);

var appSettings = appSettingSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.SecretToken);

services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(d =>
    {
        d.RequireHttpsMetadata = false;
        d.SaveToken = true;
        d.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

services.AddCors(options =>
{
    options.AddPolicy(name: MyCors, builder =>
    {
        builder.WithOrigins("*");
        builder.WithHeaders("*");
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyCors);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
