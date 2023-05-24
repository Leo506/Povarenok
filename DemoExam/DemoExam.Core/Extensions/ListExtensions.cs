namespace DemoExam.Core.Extensions;

public static class ListExtensions
{
    public static void AddIfNotNull<T>(this List<T> list, T? item)
    {
        if (item is not null)
            list.Add(item);
    }
}