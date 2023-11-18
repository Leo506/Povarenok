using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Network.Endpoints;

public class ProductsListEndpoint : ApiEndpoint<List<Product>>
{
    public ProductsListEndpoint() : base(HttpMethod.Get, "/Products")
    {
    }
}