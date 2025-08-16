using EduAtendance.WebAPI.Context;
using EduAtendance.WebAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    {
        opt.UseSqlServer("Data Source=DESKTOP-EA5G3OE;Initial Catalog=EduAttendaceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    });

builder.Services.AddControllers();
 
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentDtoValidator>();
var app = builder.Build();



app.MapOpenApi();

app.MapScalarApiReference();
 
app.MapControllers();

app.Run();
