using DemoExam.Core.Repositories;

namespace DemoExam.Core.Services.Categories;

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