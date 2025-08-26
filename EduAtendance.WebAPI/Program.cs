using EduAtendance.WebAPI.Context;
using EduAtendance.WebAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    {
        opt.UseSqlServer("Data Source=DESKTOP-EA5G3OE;Initial Catalog=EduAttendaceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    });

builder.Services.AddControllers();
 
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentDtoValidator>();

builder.Services.AddRateLimiter(conf => {
    conf.AddFixedWindowLimiter("fiexd", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(1);
        opt.PermitLimit = 100;
        opt.QueueLimit = 100;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddCors();
var app = builder.Build();

//builder.Services.AddFluentEmail("fatma2197233@gmail.com").AddSmtpSender("localhost", 25);

app.MapOpenApi();

app.MapScalarApiReference();
 app.UseCors(x => x
 .AllowAnyHeader()
 .AllowAnyOrigin()
 .AllowAnyMethod());


app.MapControllers();

app.Run();
