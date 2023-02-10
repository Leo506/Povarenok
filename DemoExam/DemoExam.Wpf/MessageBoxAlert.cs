using System.Windows;
using DemoExam.Core.Services.Alert;

namespace DemoExam.Wpf;

public class MessageBoxAlert : IAlert
{
    public void Alert(string title, string message)
    {
        MessageBox.Show(message, title);
    }

    public ChoiceResult ShowChoice(string title, string message)
    {
        var buttons = MessageBoxButton.YesNo;
        var result = MessageBox.Show(message, title, buttons);
        return result == MessageBoxResult.Yes ? ChoiceResult.Positive : ChoiceResult.Negative;
    }
}