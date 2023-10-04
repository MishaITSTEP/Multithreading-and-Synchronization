using PropertyChanged;

namespace Sync_Client
{
    [AddINotifyPropertyChangedInterface]
    public class ClientMessage
    {
        public ClientMessage(string message)
        {
            this.message=message;
        }

        public string message { get; set; }
    }
}
