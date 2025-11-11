using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentMatch.Core.DTOs.Application.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Response;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;

namespace TalentMatch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsServiceController : ControllerBase
    {
        #region Attributes

        private readonly IApplicationsService _applicationsService;

        #endregion Attributes

        #region Builder

        public ApplicationsServiceController(IApplicationsService applicationsService)
        {
            _applicationsService = applicationsService;
        }

        #endregion Builder

        /// <summary>
        /// Crea una nueva aplicación (perfil) de un empleador.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite registrar la información inicial del empleador, incluyendo
        /// datos como nombre de la empresa, industria, ciudad, número de empleados, teléfono,
        /// sitio web y descripción general.
        /// </remarks>
        /// <param name="create">Objeto que contiene los datos necesarios para crear la aplicación del empleador.</param>
        /// <returns>Indica si la creación fue exitosa.</returns>
        [HttpPost("CreateApplication")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationDtoRequest create)
        {
            return Ok(await _applicationsService.CreateApplication(create).ConfigureAwait(false));
        }

        /// <summary>
        /// Obtiene la aplicación (perfil) del empleador asociada a un Job Id.
        /// </summary>
        /// <remarks>
        /// Devuelve la información del perfil vinculada al identificador del trabajo (Job Id),
        /// permitiendo consultar el perfil asociado a una oferta o publicación específica.
        /// </remarks>
        /// <param name="jobId">Identificador del trabajo (Job Id) relacionado a la aplicación del empleador.</param>
        /// <returns>Datos del perfil del empleador.</returns>
        [HttpGet("GetApplicationByJobId")]
        [ProducesResponseType(typeof(Response<GetJobSeekerProfileDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplicationByJobId(int jobId)
        {
            return Ok(await _applicationsService.GetApplicationByJobId(jobId).ConfigureAwait(false));
        }

        /// <summary>
        /// Obtiene la aplicación (perfil) del empleador asociada a un User Id.
        /// </summary>
        /// <remarks>
        /// Devuelve la información completa del perfil correspondiente al usuario que creó
        /// la aplicación (perfil de empleador).
        /// </remarks>
        /// <param name="userId">Identificador del usuario dueño de la aplicación.</param>
        /// <returns>Datos del perfil del empleador.</returns>
        [HttpGet("GetApplicationByUserId")]
        [ProducesResponseType(typeof(Response<GetJobSeekerProfileDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplicationByUserId(int userId)
        {
            return Ok(await _applicationsService.GetApplicationByUserId(userId).ConfigureAwait(false));
        }
    }
}