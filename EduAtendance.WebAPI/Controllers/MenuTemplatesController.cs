using EduAtendance.WebAPI.Context;
using EduAtendance.WebAPI.Dtos;
using EduAtendance.WebAPI.Models;
using EduAtendance.WebAPI.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduAtendance.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuTemplatesController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public MenuTemplatesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // ---------------- GET ALL ----------------
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _dbContext.MenuTemplates
            .Select(s => new { Id = s.Id, Title = s.Title })
            .ToListAsync(cancellationToken);

        return Ok(result);
    }

    // ---------------- GET BY ID ----------------
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var menuTemplate = await _dbContext.MenuTemplates
            .Include(s => s.Submenus)
            .ThenInclude(c => c.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (menuTemplate == null)
            return NotFound(Result.Fail("Kayıt bulunamadı"));

        return Ok(menuTemplate);
    }

    // ---------------- CREATE ----------------
    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuTemplateDto createMenuTemplateDto, CancellationToken cancellationToken)
    {
        MenuTemplate menuTemplate = new()
        {
            Title = createMenuTemplateDto.Title,
            Submenus = createMenuTemplateDto.Submenus.Select(s => new MenuTemplateSubmenu
            {
                Title = s.Title,
                Items = (s.Items ?? new List<string>())
    .Select(i => new MenuTemplateSubmenuItems(i))
    .ToList()

            }).ToList()
        };

        _dbContext.Add(menuTemplate);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(Result.Succeed("Kaydedildi"));
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateMenuTemplateDto updateDto, CancellationToken cancellationToken)
    {
        var menuTemplate = await _dbContext.MenuTemplates
            .Include(s => s.Submenus)
            .ThenInclude(c => c.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (menuTemplate == null)
            return NotFound(Result.Fail("Kayıt bulunamadı"));

        // Güncelleme
        menuTemplate.Title = updateDto.Title;

        // Submenus güncelle
        menuTemplate.Submenus.Clear();
        foreach (var s in updateDto.Submenus)
        {
            menuTemplate.Submenus.Add(new MenuTemplateSubmenu
            {
                Title = s.Title,
                Items = (s.Items ?? new List<string>())
    .Select(i => new MenuTemplateSubmenuItems(i)).ToList()
            });
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Ok(Result.Succeed("Güncellendi"));
    }

    // ---------------- DELETE ----------------
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var menuTemplate = await _dbContext.MenuTemplates.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (menuTemplate == null)
            return NotFound(Result.Fail("Kayıt bulunamadı"));

        _dbContext.MenuTemplates.Remove(menuTemplate);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(Result.Succeed("Silindi"));
    }
}
