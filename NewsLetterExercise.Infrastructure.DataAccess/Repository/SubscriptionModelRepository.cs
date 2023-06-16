using NewsLetterExercise.Core.DomainModel;
using NewsLetterExercise.Core.DomainServices;
using System.Data.SqlClient;
using Dapper;
using DbSubscriptionModel = NewsLetterExercise.Infrastructure.DataAccess.Model.SubscriptionModel;

namespace NewsLetterExercise.Infrastructure.DataAccess.Repository
{
    public class SubscriptionModelRepository : ISubscriptionModelRespository
    {
        private readonly string _connectionString;

        public SubscriptionModelRepository(ConnectionString connectionString)
        {
            _connectionString = connectionString.Value;
        }

        public async Task<bool> Create(SubscriptionModel subscription)
        {
            await using var conn = new SqlConnection(_connectionString);
            const string insert =
                "INSERT INTO Subscription (Id, Name, Email, ConfirmationCode, IsConfirmed) VALUES (@Id, @Name, @Email, @ConfirmationCode, @IsConfirmed)";
            var dbSubscription = MapToDb(subscription);
            var rowsAffected = await conn.ExecuteAsync(insert, dbSubscription);

            return rowsAffected == 1;
        }

        

        public async Task<SubscriptionModel> Read(string email)
        {
            await using var conn = new SqlConnection(_connectionString);
            const string select =
                "SELECT Id, Name, Email, ConfirmationCode, IsConfirmed from Subscription WHERE Email=@Email";
            var result = await conn.QueryAsync<DbSubscriptionModel>(select, new { Email = email });
            var dbSubscription = result.SingleOrDefault();
            
            return MapToDomain(dbSubscription);
        }

        public async Task<bool> Update(SubscriptionModel subscription)
        {
            await using var conn = new SqlConnection(_connectionString);
            const string insert =
                "UPDATE Subscription SET IsConfirmed=@IsConfirmed WHERE Id=@Id";
            var dbSubscription = MapToDb(subscription);
            var rowsAffected = await conn.ExecuteAsync(insert, dbSubscription);
            
            return rowsAffected == 1;
        }

        private SubscriptionModel MapToDomain(DbSubscriptionModel dbSubscription)
        {
            return new SubscriptionModel(dbSubscription.Id, dbSubscription.Name, dbSubscription.Email, dbSubscription.ConfirmationCode, dbSubscription.IsConfirmed);
        }

        private DbSubscriptionModel MapToDb(SubscriptionModel subscription)
        {
            return new DbSubscriptionModel(subscription.Id, subscription.Name, subscription.Email, subscription.ConfirmationCode, subscription.IsConfirmed);
        }
    }
}
