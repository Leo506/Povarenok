namespace DemoExam.Core.Models;

public class DiscountSelectableItem
{
    public double? MinDiscount { get; set; }
    public double? MaxDiscount { get; set; }

    public Func<double, bool> GetDiscountSortPredicate()
    {
        if (MaxDiscount is not null)
            return discount => discount >= MinDiscount && discount < MaxDiscount;
        if (MinDiscount is not null)
            return discount => discount >= MinDiscount;

        return _ => true;
    }

    public override string ToString()
    {
        if (MinDiscount is null && MaxDiscount is null)
            return "All";
        return $"{MinDiscount}%{(MaxDiscount is null ? " and more" : $" - {MaxDiscount}%")}";
    }
}