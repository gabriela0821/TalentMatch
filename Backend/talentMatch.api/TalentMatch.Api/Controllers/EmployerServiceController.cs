using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TalentMatch.Core.DTOs.EmployerProfile.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Response;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;

namespace TalentMatch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerServiceController : ControllerBase
    {
        #region Attributes

        private readonly IEmployerService _employerService;

        #endregion Attributes

        #region Builder

        public EmployerServiceController(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        #endregion Builder

        /// <summary>
        /// Permite crear el perfil de un Employer.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite a un usuario tipo empleador crear su perfil proporcionando información básica como nombre de la empresa, industria, ciudad, número de empleados, teléfono, sitio web y descripción de la empresa.
        /// </remarks>
        /// <param name="create">Objeto que contiene los datos necesarios para crear el perfil del empleador.</param>
        /// <returns>Respuesta indicando si la creación fue exitosa.</returns>
        [HttpPost("CreateProfile")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProfile([FromBody] CreateEmployerProfileDtoRequest create)
        {
            return Ok(await _employerService.CreateProfile(create).ConfigureAwait(false));
        }

        /// <summary>
        /// Permite actualizar el perfil de un Employer.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite al usuario tipo empleador modificar la información de su perfil previamente creada.
        /// </remarks>
        /// <param name="update">Objeto con los datos actualizados del perfil del empleador.</param>
        /// <returns>Respuesta indicando si la actualización fue exitosa.</returns>
        [HttpPost("UpdateProfile")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateEmployerProfileDtoRequest update)
        {
            return Ok(await _employerService.UpdateProfile(update).ConfigureAwait(false));
        }

        /// <summary>
        /// Permite obtener el perfil de un Employer por Id de usuario.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve la información completa del perfil asociado a un usuario empleador específico.
        /// </remarks>
        /// <param name="userId">Id del usuario cuyo perfil de empleador se desea consultar.</param>
        /// <returns>Respuesta con los datos del perfil del empleador.</returns>
        [HttpGet("Profile")]
        [ProducesResponseType(typeof(Response<GetJobSeekerProfileDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProfileById([Required][FromHeader]int userId)
        {
            return Ok(await _employerService.GetProfileById(userId).ConfigureAwait(false));
        }
    }
}