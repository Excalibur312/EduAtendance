using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduAtendance.WebAPI.Context;
using EduAtendance.WebAPI.Dtos;
using EduAtendance.WebAPI.Models;
using FluentValidation.Results;
using EduAtendance.WebAPI.Tools;
using Microsoft.EntityFrameworkCore;
namespace EduAtendance.WebAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SurveyTemplatesController: ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public SurveyTemplatesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpPost]

    public async Task<IActionResult> CreateSurveyTemplate(CreateSurveyTemplateDto request , CancellationToken cancellationToken)
    {
        SurveyTemplate surveyTemplate = new()
        {
            Title = request.Title,
            Categories = request.Categories.Select(c => new SurveyTemplateCategory
            {
                Title = c.Title,
                Options = c.Option.Select(c => new SurveyTemplateCategoryOption(c)).ToList(),
                Questions = c.Questions.Select(c => new SurveyTemplateCategoryQuestion(c)).ToList()
            }).ToList()

        };
        _dbContext.Add(surveyTemplate);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(Result.Succeed("Kaydedildi"));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSurveyTemplates(CancellationToken cancellationToken)
    {
        var Result = await _dbContext.SurveyTemplates.Select(s => new { Id = s.Id, Title = s.Title }).ToListAsync(cancellationToken);
        return Ok(Result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get (Guid id, CancellationToken cancellationToken)
    {
        var res = await _dbContext.SurveyTemplates
     .AsNoTracking()
     .Where(s => s.Id == id)
     .Select(s => new
     {
         Id = s.Id,
         Title = s.Title,
         Categories = s.Categories
     })
     .FirstOrDefaultAsync(cancellationToken);

        return Ok(res);
    }
}
