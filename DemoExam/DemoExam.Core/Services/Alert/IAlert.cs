namespace DemoExam.Core.Services.Alert;

public enum ChoiceResult
{
    Positive,
    Negative
}

public interface IAlert
{
    void Alert(string title, string message);

    ChoiceResult ShowChoice(string title, string message);
}