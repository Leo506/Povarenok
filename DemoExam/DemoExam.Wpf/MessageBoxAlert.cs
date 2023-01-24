using System.Windows;
using DemoExam.Core.Services.Alert;

namespace DemoExam.Wpf;

public class MessageBoxAlert : IAlert
{
    public void Alert(string title, string message)
    {
        MessageBox.Show(message, title);
    }
}