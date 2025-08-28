using EduAtendance.WebAPI.Models;

namespace EduAtendance.WebAPI.Dtos;

public sealed record CreateMenuTemplateDto(
    string Title,
    List<CreateSubmenuDto> Submenus
);

public sealed record CreateSubmenuDto(
    string Title,
    List<CreateSubmenuDto>? Submenus,  
    List<string>? Items  
);
