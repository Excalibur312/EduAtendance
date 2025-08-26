namespace EduAtendance.WebAPI.Models;

public class Survey
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Subtitle { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

     
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Order { get; set; }

    
    public Survey Survey { get; set; } = default!;
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public AnswerType AnswerType { get; set; } = AnswerType.SingleChoice;
    public int Order { get; set; }

    
    public Category Category { get; set; } = default!;
    public ICollection<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
}

public class AnswerOption
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public int? Score { get; set; }
    public int Order { get; set; }

    
    public Question Question { get; set; } = default!;
}

public enum AnswerType
{
    SingleChoice = 0,
    MultiChoice = 1,
    FreeText = 2,
    Rating = 3
}
