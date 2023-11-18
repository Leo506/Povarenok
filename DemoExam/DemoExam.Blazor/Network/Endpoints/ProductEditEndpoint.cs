using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Requests;

namespace DemoExam.Blazor.Network.Endpoints;

public class ProductEditEndpoint : ApiEndpoint
{
    private readonly ProductEdit _productEdit;

    public ProductEditEndpoint(string article, ProductEdit productEdit) : base(HttpMethod.Post, $"/Products/edit/{article}")
    {
        _productEdit = productEdit;
    }

    public override HttpContent? CreateContent() => ToJson(_productEdit);
}