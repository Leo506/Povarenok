using DemoExam.Core.ObservableObjects;
using MvvmCross.Commands;

namespace DemoExam.Core.Models;

public record ProductOperation(string OperationName, IMvxCommand<ObservableProduct> Operation)
{
    public override string ToString()
    {
        return OperationName;
    }
}