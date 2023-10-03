namespace DemoExam.Domain.Services.Categories;

public interface ICategoriesService
{
    Task<IEnumerable<string>> GetAll();
}