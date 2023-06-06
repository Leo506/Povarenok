namespace DemoExam.Core.Services.Categories;

public interface ICategoriesService
{
    Task<IEnumerable<string>> GetAll();
}