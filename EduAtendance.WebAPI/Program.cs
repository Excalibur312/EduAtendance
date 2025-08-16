using EduAtendance.WebAPI.Context;
using EduAtendance.WebAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    {
        opt.UseSqlServer("YOURSERVER");

    });

builder.Services.AddControllers();
 
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentDtoValidator>();
var app = builder.Build();



app.MapOpenApi();

app.MapScalarApiReference();
 
app.MapControllers();

app.Run();
