using Arbus.Network;

namespace DemoExam.Blazor.Network;

public class NativeHttpClient : INativeHttpClient
{
    private readonly HttpClient _httpClient;

    public NativeHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseHeadersRead) =>
        _httpClient.SendAsync(request, httpCompletionOption, cancellationToken);

    public Task<string> GetString(string uri, TimeSpan? timeout = null) => throw new NotImplementedException();

    public Task<string> GetString(Uri uri, TimeSpan? timeout = null) => throw new NotImplementedException();
}