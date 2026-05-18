using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TestAO1145Api;
using static TestAO1145Api.Controllers.AuthController;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       // óêàçûâàåò, áóäåò ëè âàëèäèðîâàòüñÿ èçäàòåëü ïðè âàëèäàöèè òîêåíà
                       ValidateIssuer = true,
                       // ñòðîêà, ïðåäñòàâëÿþùàÿ èçäàòåëÿ
                       ValidIssuer = AuthOptions.ISSUER,
                       // áóäåò ëè âàëèäèðîâàòüñÿ ïîòðåáèòåëü òîêåíà
                       ValidateAudience = true,
                       // óñòàíîâêà ïîòðåáèòåëÿ òîêåíà
                       ValidAudience = AuthOptions.AUDIENCE,
                       // áóäåò ëè âàëèäèðîâàòüñÿ âðåìÿ ñóùåñòâîâàíèÿ
                       ValidateLifetime = true,
                       // óñòàíîâêà êëþ÷à áåçîïàñíîñòè
                       IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                       // âàëèäàöèÿ êëþ÷à áåçîïàñíîñòè
                       ValidateIssuerSigningKey = true,
                   };
               });
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(s => s.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{ // âñÿ ýòà ëÿìáäà äëÿ ïðîäàêøåíà íå íóæíà
  //c.SwaggerDoc("v1", new Info { Title = "You api title", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Àâòîðèçàöèÿ, óêàæèòå êîä â ôîðìàòå: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
});

builder.Services.AddDbContext<Testao1145Context>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
