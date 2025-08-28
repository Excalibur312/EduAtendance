namespace EduAtendance.WebAPI.Dtos;

public sealed record UpdateMenuTemplateDto(
    string Title,
    List<UpdateSubmenuDto> Submenus
);

public sealed record UpdateSubmenuDto(
    string Title,
    List<string>? Items
);
