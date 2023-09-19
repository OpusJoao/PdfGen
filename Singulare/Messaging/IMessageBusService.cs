namespace Singulare.Messaging
{
    public interface IMessageBusService
    {
        void publish(object data, string routingKey);
    }
}
