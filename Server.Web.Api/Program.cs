using Microsoft.AspNetCore.Authentication.Cookies;
using Server.Web.Api.Helper;
using Server.Web.BL.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAllServices();

// add database config
builder.Services.AddDbConfig(builder.Configuration);

// add mapper config
builder.Services.AddMapperConfig();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI
builder.Services.AddSwaggerConfig();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder1 =>
        builder1.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
            
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme =
        Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

builder.Services.AddOurAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Chat v1"));
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();