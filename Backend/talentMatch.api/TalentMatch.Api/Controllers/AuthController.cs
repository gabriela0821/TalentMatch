using Microsoft.AspNetCore.Mvc;
using TalentMatch.Core.DTOs.User.Request;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;

namespace TalentMatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Attributes

        private readonly IAuthService _authService;

        #endregion Attributes

        #region Builder

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion Builder

        /// <summary>
        /// Permite iniciar sesión y obtener un token JWT.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite a un usuario existente iniciar sesión proporcionando su correo electrónico y contraseña.
        /// Si las credenciales son correctas, se devuelve un JWT que debe incluirse en las solicitudes autorizadas posteriores.
        /// </remarks>
        /// <param name="loginUser">Objeto que contiene el Email y Password del usuario.</param>
        /// <returns>Respuesta con los datos del usuario y el token JWT.</returns>
        [HttpPost("LoginUser")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDtoRequest loginUser)
        {
            return Ok(await _authService.LoginUserAsync(loginUser).ConfigureAwait(false));
        }

        /// <summary>
        /// Permite registrar un nuevo usuario.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite crear un usuario nuevo proporcionando correo electrónico, contraseña y tipo de usuario (JobSeeker o Employer).
        /// Una vez registrado, el usuario podrá iniciar sesión y recibir un token JWT para autenticación.
        /// </remarks>
        /// <param name="createUser">Objeto con los datos del nuevo usuario.</param>
        /// <returns>Respuesta con los datos del usuario registrado.</returns>
        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserDtoRequest createUser)
        {
            return Ok(await _authService.RegisterUserAsync(createUser).ConfigureAwait(false));
        }
    }
}