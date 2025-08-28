namespace EduAtendance.WebAPI.Models;

public sealed class MenuTemplateSubmenu
{
    public string Title { get; set; } = default!;
    public List<MenuTemplateSubmenu> Submenus { get; set; } = new();
    public List<MenuTemplateSubmenuItems> Items { get; set; } = new();
}
