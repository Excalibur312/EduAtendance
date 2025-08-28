namespace EduAtendance.WebAPI.Models;

public sealed class MenuTemplate
{
    public MenuTemplate()
    {
         Id = Guid.CreateVersion7();
    }

    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public List<MenuTemplateSubmenu> Submenus { get; set; } = new();
}
