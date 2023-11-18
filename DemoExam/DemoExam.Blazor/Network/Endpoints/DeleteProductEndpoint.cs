using Arbus.Network;

namespace DemoExam.Blazor.Network.Endpoints;

public class DeleteProductEndpoint : ApiEndpoint
{
    public DeleteProductEndpoint(string productArticle) : base(HttpMethod.Delete, $"Products/{productArticle}")
    {
    }
}