
using NewsLetterExercise.Core.DomainModel;

namespace NewsLetterExercise.Core.DomainServices
{
    public interface ISubscriptionModelRespository
    {
        Task<bool> Create(SubscriptionModel subscriptionModel);
        Task<SubscriptionModel> Read(string email);
        Task<bool> Update(SubscriptionModel subscriptionModel);
    }
}
