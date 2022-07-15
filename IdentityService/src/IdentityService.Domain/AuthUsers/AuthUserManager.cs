using IdentityService.AuthUsers.Repositories;
using IdentityService.DTOs.inputs;
using System;
using System.Threading;
using System.Threading.Tasks;

using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace IdentityService.AuthUsers
{
    public class AuthUserManager : DomainService
    {
        private readonly IAuthUserRepository _authUserRepository;
        private readonly IGuidGenerator _guidGenerator;

        public AuthUserManager(IAuthUserRepository authUserRepository, IGuidGenerator guidGenerator)
        {
            _authUserRepository = authUserRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task<Guid> CreateAsync(AuthUserInputDto input, CancellationToken cancellationToken)
        {
            await ValidateUserCreationAsync(input, cancellationToken);

            var id = _guidGenerator.Create();

            var user = new AuthUser(id, input.Cellphone, input.Email);

            await _authUserRepository.InsertAsync(user, autoSave: true, cancellationToken);

            return id;
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                throw new AuthUserException("Id is not valid");

            var user = await _authUserRepository.GetAsync(id);

            if (user is null)
                throw new AuthUserException("UserNotFound");

            user.Remove();

            await _authUserRepository.RemoveAsync(user, cancellationToken);
        }

        private async Task ValidateUserCreationAsync(AuthUserInputDto input, CancellationToken cancellationToken)
        {
            if (input is null)
                throw new AuthUserException("input is null");

            var cellphneIsDuplicated = await _authUserRepository.IsExistAsync(input.Cellphone, cancellationToken);

            if (cellphneIsDuplicated)
                throw new AuthUserException("cellphone is duplicated");
        }
    }
}