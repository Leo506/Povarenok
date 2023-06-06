namespace DemoExam.Core.Repositories;

public interface ICategoriesRepository
{
    Task<IEnumerable<string>> GetAll();
}