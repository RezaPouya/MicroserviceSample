using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs.inputs;
using UserService.Users.Repositories;
using Volo.Abp.Domain.Services;

namespace UserService.Users
{
    public class UserManager : DomainService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(UserInputDto input, CancellationToken cancellationToken)
        {
            await ValidateUserCreationAsync(input, cancellationToken);

            var user = new User(input.Id, input.Fname, input.Lname);

            await _userRepository.InsertAsync(user, autoSave: true, cancellationToken);
        }


        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                throw new UserException(code: UserServiceErrorCodes.UserIdNotValid).WithData("Id", id);

            var user = await _userRepository.GetAsync(id);

            if(user is null)
                throw new UserException(code: UserServiceErrorCodes.UserNotFound);

            user.Remove();

            await _userRepository.RemoveAsync(user, cancellationToken);
        }

        private async Task ValidateUserCreationAsync(UserInputDto input, CancellationToken cancellationToken)
        {
            if (input is null)
                throw new UserException(code: UserServiceErrorCodes.UserInputNotValid);

            if (input.Id == Guid.Empty)
                throw new UserException(code: UserServiceErrorCodes.UserIdNotValid);

            bool isUserWithThisIdAlreadyCreated = await _userRepository.IsExistAsync(input.Id, cancellationToken);

            if (isUserWithThisIdAlreadyCreated)
                throw new UserException(code: UserServiceErrorCodes.UserIdDuplicated).WithData("Id", input.Id);
        }
    }
}