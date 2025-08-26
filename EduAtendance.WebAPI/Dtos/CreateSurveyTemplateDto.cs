namespace EduAtendance.WebAPI.Dtos;

public sealed record CreateSurveyTemplateDto(string Title, List<SurveyTemplateCategoryDto> Categories);



public sealed record SurveyTemplateCategoryDto(
    string Title,
    List<string> Questions,
    List<string> Option);
 