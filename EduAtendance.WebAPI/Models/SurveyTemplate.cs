namespace EduAtendance.WebAPI.Models;

public class SurveyTemplate
{

    public SurveyTemplate()
    {
        Id = Guid.CreateVersion7();
    }

    public Guid Id { get; set; } 
    public string Title { get; set; } = default!;

    public List<SurveyTemplateCategory> Categories { get; set; } = new();
}


public sealed class SurveyTemplateCategory
{
    public string Title { get; set; } = default!;   

    public List<SurveyTemplateCategoryQuestion> Questions { get; set; } = new();

    public List<SurveyTemplateCategoryOption> Options { get; set; } = new();
}



