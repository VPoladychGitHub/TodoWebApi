using System.Reflection;
using FluentValidation;
using TodoWebApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Diagnostics;
using TodoWebApi.Models.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

const string tokenKey = "1hjdcmjdjkfckjvmvmkjvgmmvgmkvfjkf";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<TodoService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication().
AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "localhost",
        ValidAudience = "localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
builder.Services.AddAuthorization();

builder.Services.AddProblemDetails();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");
var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("uk"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    ApplyCurrentCultureToResponseHeaders = true
});

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/getToken",  (string login, string password) => {

    if (login != "qwe" || password != "123")
    {
        return Results.Unauthorized();
    }
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
                new Claim("Id", "123"),
                new Claim("Name", login),

             }),
        Expires = DateTime.UtcNow.AddSeconds(25),
        Issuer = "localhost",
        Audience = "localhost",
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)), SecurityAlgorithms.HmacSha512Signature)
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    var jwtToken = tokenHandler.WriteToken(token);
    var stringToken = tokenHandler.WriteToken(token);
    return Results.Ok(stringToken);
});

app.Map( "/error",(HttpContext context) =>
{
    var execption = context.Features.Get<IExceptionHandlerPathFeature>()!.Error;
    IResult result = execption switch
    {
        NotFaundTodo notFaund => Results.Problem(statusCode: 400, title: $"record : {notFaund.EntityName}  not faund", extensions: new Dictionary<string, object?> { { "trace-id", Activity.Current?.Id } }),
        ValidationException validationError => Results.Problem(statusCode: 400, title: validationError.Message,extensions:new Dictionary<string, object?> { { "trace-id", Activity.Current?.Id} }),
        _ => Results.Problem( extensions: new Dictionary<string, object?> { { "trace-id", Activity.Current?.Id } }),
    };

    return result;
});
app.MapControllers();

app.Run();
