namespace DemoExam.Domain.Exceptions;

public class InvalidProductsAmountInOrderException : Exception
{
    public InvalidProductsAmountInOrderException() : base("Invalid products amount in order")
    {
        
    }   
}