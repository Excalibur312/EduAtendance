using EduAtendance.WebAPI.Context;
using EduAtendance.WebAPI.Dtos;
using EduAtendance.WebAPI.Models;
using EduAtendance.WebAPI.Tools;
using EduAtendance.WebAPI.Validators;
using FluentEmail.Core;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 

namespace EduAtendance.WebAPI.Controllers;

[Route("[Controller]")]
[ApiController]
public sealed class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    Result res = default!;

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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get (Guid id, CancellationToken cancellationToken)
    {
        Student? student = await _dbContext.Students.FirstOrDefaultAsync( p => p.Id == id, cancellationToken);
        if (student == null)
        {
            res = Result.Fail("Öğrenci yok");
            return BadRequest(res);
        }
       
        return Ok(Result.Succeed("Getirildi"));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentDto request, CancellationToken cancellationToken)
    {

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
            res = Result.Fail("Öğrencinin TC'si daha önce kaydedilmiş");
            return BadRequest(res);
        }

        Student student = request.Adapt<Student>();
        _dbContext.Add(student);
        await _dbContext.SaveChangesAsync(cancellationToken);


        Result.Succeed("Kayıt Tamamlandı");
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStudentDto request, CancellationToken cancellationToken)
    {
        UpdateStudentDtoValidator validator = new();
        ValidationResult validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            Result.Fail(validationResult.Errors.Select(s => s.ErrorMessage).ToList());
            return BadRequest(res);
        }

        var isIdentityNumberExists = await _dbContext.Students
            .AnyAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken);


        Student student = request.Adapt<Student>();
        _dbContext.Update(student);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Ok(Result.Succeed("başarılı"));


    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        Student? student = await _dbContext.Students.FirstOrDefaultAsync( p => p.Id == id, cancellationToken);
        if (student == null)
        {
            res = Result.Fail("Öğrenci yok");
            return BadRequest(res);
        }
        _dbContext.Remove(student);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Ok(Result.Succeed("Silindi"));
    }
}