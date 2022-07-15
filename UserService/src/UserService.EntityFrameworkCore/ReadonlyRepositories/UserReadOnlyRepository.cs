using Dapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs;
using UserService.EntityFrameworkCore;
using UserService.ReadonlyRepositories.Scripts.Users;
using UserService.Users.Repositories;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace UserService.ReadonlyRepositories
{
    public class UserReadOnlyRepository : DapperRepository<UserServiceDbContext>, IUserReadOnlyRepository
    {
        public UserReadOnlyRepository(IDbContextProvider<UserServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<UserOutputDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("userId", id);

            var dbConnection = await GetDbConnectionAsync();

            var command = new CommandDefinition(commandText: User_SqlScript.GetById,
                parameters: parameters,
                commandType: System.Data.CommandType.Text,
                cancellationToken: cancellationToken);

            return await dbConnection.QueryFirstOrDefaultAsync<UserOutputDto>(command);
        }
    }
}