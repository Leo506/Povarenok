using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Network.Endpoints;

public class ProductEndpoint : ApiEndpoint<Product>
{
    public ProductEndpoint(string article) : base(HttpMethod.Get, $"Catalog/{article}")
    {
    }
}