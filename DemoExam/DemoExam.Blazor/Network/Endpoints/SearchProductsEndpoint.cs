using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Network.Endpoints;

public class SearchProductsEndpoint : ApiEndpoint<List<Product>>
{
    public SearchProductsEndpoint(string? searchString, string? categoryFilter) 
        : base(HttpMethod.Get, $"catalog/search?searchString={searchString}&category={categoryFilter}")
    {
    }
}