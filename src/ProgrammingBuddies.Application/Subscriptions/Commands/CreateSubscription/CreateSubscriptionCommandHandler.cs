using ErrorOr;

using MediatR;

using ProgrammingBuddies.Application.Common.Interfaces;
using ProgrammingBuddies.Application.Subscriptions.Common;
using ProgrammingBuddies.Domain.Subscriptions;
using ProgrammingBuddies.Domain.Users;

namespace ProgrammingBuddies.Application.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler(
        IUsersRepository _usersRepository) : IRequestHandler<CreateSubscriptionCommand, ErrorOr<SubscriptionResult>>
    {
        public async Task<ErrorOr<SubscriptionResult>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            if (await _usersRepository.GetByIdAsync(request.UserId, cancellationToken) is not null)
            {
                return Error.Conflict(description: "User already has an active subscription");
            }

            var subscription = new Subscription(request.SubscriptionType);

            var user = new User(
                request.UserId,
                request.FirstName,
                request.LastName,
                request.Email,
                subscription);

            await _usersRepository.AddAsync(user, cancellationToken);

            return SubscriptionResult.FromUser(user);
        }
    }
}
