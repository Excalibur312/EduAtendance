using EduAtendance.WebAPI.Context;
using EduAtendance.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduAtendance.WebAPI.Dtos;
using Mapster;
using EduAtendance.WebAPI.Validators;
using FluentValidation.Results;
using EduAtendance.WebAPI.Tools;

namespace EduAtendance.WebAPI.Controllers;

[Route("[Controller]")]
[ApiController]
public sealed class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public StudentsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        List<Student> students = await _dbContext.Students.ToListAsync(cancellationToken);

        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentDto request, CancellationToken cancellationToken)
    {
        var res = new Result();
        CreateStudentDtoValidator validator = new();
        ValidationResult validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            Result.Fail(validationResult.Errors.Select(s => s.ErrorMessage).ToList());
            return BadRequest(res);
        }

        var isIdentityNumberExists = await _dbContext.Students
            .AnyAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken);

        if (isIdentityNumberExists)
        {
            
            return BadRequest(Result.Fail("Öğrencinin TC'si daha önce kaydedilmiş"));
        }

        Student student = request.Adapt<Student>();
        _dbContext.Add(student);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Ok(Result.Succeed("Kayıt Tamamlandı"));
    }

    

}
