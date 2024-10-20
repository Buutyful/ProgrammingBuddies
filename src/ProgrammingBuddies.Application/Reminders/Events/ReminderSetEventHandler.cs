using MediatR;

using ProgrammingBuddies.Application.Common.Interfaces;
using ProgrammingBuddies.Domain.Users.Events;

namespace ProgrammingBuddies.Application.Reminders.Events
{
    public class ReminderSetEventHandler(IRemindersRepository _remindersRepository) : INotificationHandler<ReminderSetEvent>
    {
        public async Task Handle(ReminderSetEvent @event, CancellationToken cancellationToken)
        {
            await _remindersRepository.AddAsync(@event.Reminder, cancellationToken);
        }
    }
}
