using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TalentMatch.Core.DTOs.Certification.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Response;
using TalentMatch.Core.DTOs.WorkExperience.Request;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;

namespace TalentMatch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        #region Attributes

        private readonly IJobSeekerService _jobSeekerService;

        #endregion Attributes

        #region Builder

        public JobSeekerController(IJobSeekerService jobSeekerService)
        {
            _jobSeekerService = jobSeekerService;
        }

        #endregion Builder

        /// <summary>
        /// Permite crear el perfil de un Job Seeker.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite a un usuario crear su perfil proporcionando información básica como nombre, edad, ciudad, nivel educativo, habilidades y salario esperado.
        /// </remarks>
        /// <param name="create">Objeto que contiene los datos necesarios para crear el perfil.</param>
        /// <returns>Respuesta indicando si la creación fue exitosa.</returns>
        [HttpPost("CreateProfile")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProfile([FromBody] CreateJobSeekerProfileDtoRequest create)
        {
            return Ok(await _jobSeekerService.CreateProfile(create).ConfigureAwait(false));
        }

        /// <summary>
        /// Permite actualizar el perfil de un Job Seeker.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite al usuario modificar la información de su perfil previamente creada.
        /// </remarks>
        /// <param name="update">Objeto con los datos actualizados del perfil.</param>
        /// <returns>Respuesta indicando si la actualización fue exitosa.</returns>
        [HttpPost("UpdateProfile")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateJobSeekerProfileDtoRequest update)
        {
            return Ok(await _jobSeekerService.UpdateProfile(update).ConfigureAwait(false));
        }

        /// <summary>
        /// Permite obtener el perfil de un Job Seeker por Id de usuario.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve la información completa del perfil asociado a un usuario específico.
        /// </remarks>
        /// <param name="userId">Id del usuario cuyo perfil se desea consultar.</param>
        /// <returns>Respuesta con los datos del perfil.</returns>
        [HttpGet("Profile")]
        [ProducesResponseType(typeof(Response<GetJobSeekerProfileDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProfileById([Required][FromHeader] int userId)
        {
            return Ok(await _jobSeekerService.GetProfileById(userId).ConfigureAwait(false));
        }

        /// <summary>
        /// Permite agregar experiencia laboral al perfil de un Job Seeker.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite registrar una nueva experiencia laboral indicando empresa, cargo, fechas y descripción.
        /// </remarks>
        /// <param name="create">Objeto que contiene los datos de la experiencia laboral.</param>
        /// <returns>Respuesta indicando si la creación fue exitosa.</returns>
        [HttpPost("CreateExperience")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateExperience([FromBody] CreateWorkExperienceDtoRequest create)
        {
            return Ok(await _jobSeekerService.CreateExperience(create).ConfigureAwait(false));
        }

        /// <summary>
        /// Permite agregar una certificación al perfil de un Job Seeker.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite registrar certificaciones obtenidas indicando nombre, entidad emisora, fecha y URL de credencial.
        /// </remarks>
        /// <param name="create">Objeto que contiene los datos de la certificación.</param>
        /// <returns>Respuesta indicando si la creación fue exitosa.</returns>
        [HttpPost("CreateCertification")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCertification([FromBody] CreateCertificationDtoRequest create)
        {
            return Ok(await _jobSeekerService.CreateCertification(create).ConfigureAwait(false));
        }
    }
}