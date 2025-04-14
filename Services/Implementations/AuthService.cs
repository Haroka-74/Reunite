using Reunite.DTOs.AuthDTOs;
using Reunite.Models.Auth;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;

namespace Reunite.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository repository;


        public AuthService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            await repository.AddUser(new ReuniteUser
            { Id = registerDTO.Auth0Id, Username = registerDTO.Username, Email = registerDTO.Email });
        }

    }
}