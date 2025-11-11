using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TalentMatch.Core.DTOs.User.Request;
using TalentMatch.Core.DTOs.User.Response;
using TalentMatch.Core.Interfaces.Repositories;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.Entities;
using TalentMatch.Infrastructure.Exceptions;

namespace TalentMatch.Core.Features.Services
{
    public class AuthService : IAuthService
    {
        #region Attributes

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        #endregion Attributes

        #region Builder

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        #endregion Builder

        public async Task<Response<GetUserDtoResponse?>> LoginUserAsync(LoginUserDtoRequest login)
        {
            try
            {
                var user = await Task.FromResult(_unitOfWork.UserRepositoryAsync
                    .FindBy(x => x.Email.Equals(login.Email))
                    .FirstOrDefault());

                if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
                    throw new CoreException("Usuario o contraseña inválidos.");

                // Generar claims
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("role", user.UserType)
                };

                // Obtener configuración desde appsettings.json
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2), // Duración del token
                    signingCredentials: creds
                );

                var response = _mapper.Map<GetUserDtoResponse>(user);
                response.Token = new JwtSecurityTokenHandler().WriteToken(token);

                return new Response<GetUserDtoResponse>(response);
            }
            catch (Exception ex)
            {
                return new Response<GetUserDtoResponse>(succeeded: false, $"Error al procesar la aplicación: {ex}");
            }
        }

        public async Task<Response<GetUserDtoResponse?>> RegisterUserAsync(CreateUserDtoRequest create)
        {
            try
            {
                var existingUser = await Task.FromResult(_unitOfWork.UserRepositoryAsync
                    .FindBy(x => x.Email == create.Email)
                    .FirstOrDefault());

                if (existingUser != null)
                {
                    throw new CoreException("El correo ya está registrado.");
                }

                var user = new User
                {
                    Email = create.Email,
                    UserType = create.UserType,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(create.PasswordHash),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.UserRepositoryAsync.AddAsync(user);
                await _unitOfWork.CommitAsync();

                return new Response<GetUserDtoResponse>(_mapper.Map<GetUserDtoResponse>(user));
            }
            catch (Exception ex)
            {
                return new Response<GetUserDtoResponse>(succeeded: false, ex.Message);
            }
        }
    }
}