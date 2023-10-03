using DemoExam.Domain.Repositories;

namespace DemoExam.Domain.Services.Categories;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _repository;

    public CategoriesService(ICategoriesRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<string>> GetAll()
    {
        return _repository.GetAll();
    }
}