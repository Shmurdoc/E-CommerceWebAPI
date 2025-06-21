using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using eCommerceWebAPI.Data;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;
using System.Data;

namespace eCommerceWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDataContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDataContext dbContext, ILogger<UserRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"User with ID {id} not found.");
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Getting User Details: {ex}");
                throw;
            }
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"New User Created in the Database, Object: {JsonConvert.SerializeObject(user).ToUpper()}");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Creating User Details: {ex}");
                throw;
            }
        }
    }
}
