using DemoExam.Core.NotifyObjects;
using MvvmCross.Commands;

namespace DemoExam.Core.Models;

public record ProductOperation(string OperationName, MvxCommand<ProductNotifyObject> Operation)
{
    public override string ToString()
    {
        return OperationName;
    }
}