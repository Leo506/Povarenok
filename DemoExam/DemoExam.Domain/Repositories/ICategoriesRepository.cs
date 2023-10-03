namespace DemoExam.Domain.Repositories;

public interface ICategoriesRepository
{
    Task<IEnumerable<string>> GetAll();
}