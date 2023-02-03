using MvvmCross.Commands;

namespace DemoExam.Core.Models;

public record ProductOperation(string OperationName, MvxCommand<Product> Operation)
{
    public override string ToString()
    {
        return OperationName;
    }
}