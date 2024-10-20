using MediatR;

using ProgrammingBuddies.Application.Common.Interfaces;
using ProgrammingBuddies.Domain.Users.Events;

namespace ProgrammingBuddies.Application.Subscriptions.Events
{
    public class SubscriptionCanceledEventHandler(IUsersRepository _usersRepository)
        : INotificationHandler<SubscriptionCanceledEvent>
    {
        public async Task Handle(SubscriptionCanceledEvent notification, CancellationToken cancellationToken)
        {
            notification.User.DeleteAllReminders();

            await _usersRepository.RemoveAsync(notification.User, cancellationToken);
        }
    }
}
