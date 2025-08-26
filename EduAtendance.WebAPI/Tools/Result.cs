namespace EduAtendance.WebAPI.Tools;

public class Result
{

    private List<string> _Message = new();
    public IReadOnlyCollection<string> Messages => _Message.ToArray();

    public Result()
    {

    }
    public Result(string message)
    {
        Add(message);
    }

    public static Result Succeed(string message)
    {
        var res = new Result();
        res.Add(message);
        return res;
    }
    public static Result Fail(List<string> message)
    {
        var res = new Result();
        res.Add(message);
        return res;
    }
    public static Result Fail(string message)
    {
        var res = new Result();
        res.Add(message);
        return res;
    }


    public void Add(string message)
    {
        _Message.Add(message);

    }

    public void Add(List<string> messages)
    {
        _Message.AddRange(messages);
    }
}
