using Arbus.Network;

namespace DemoExam.Blazor.Network;

public class NetworkManager : INetworkManager
{
    public bool IsNetworkAvailable => true;
    public event EventHandler<bool>? NetworkAvailabilityChanged;
}