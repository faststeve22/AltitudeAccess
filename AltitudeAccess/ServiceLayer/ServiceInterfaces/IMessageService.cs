using AltitudeAccess.ServiceLayer.Models;

namespace AltitudeAccess.ServiceLayer.ServiceInterfaces
{
    public interface IMessageService
    {
        void PublishEventMessage(User createdUser);
    }
}
