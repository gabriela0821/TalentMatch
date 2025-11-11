using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TalentMatch.Core.DTOs.ApplicationQuestion.Request;
using TalentMatch.Core.DTOs.JobPosting.Request;
using TalentMatch.Core.DTOs.JobPosting.Response;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.QueryFilters;

namespace TalentMatch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        #region Attributes

        private readonly IJobPostingService _jobPostingService;

        #endregion Attributes

        #region Builder

        public JobPostingController(IJobPostingService jobPostingService)
        {
            _jobPostingService = jobPostingService;
        }

        #endregion Builder

        /// <summary>
        /// Crea una nueva vacante junto con sus preguntas opcionales.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite a un empleador crear un Job Posting proporcionando toda la información de la vacante.
        /// Si se incluyen preguntas de aplicación, se agregan automáticamente a la vacante.
        /// </remarks>
        /// <param name="create">Objeto con los datos del Job Posting y preguntas opcionales.</param>
        /// <returns>Respuesta con los datos de la vacante recién creada.</returns>
        /// <response code="500">InternalServerError. Ha ocurrido una excepción no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("CreateJobPosting")]
        [ProducesResponseType(typeof(Response<GetJobPostingDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateJobPosting([FromBody] CreateJobPostingDtoRequest create)
        {
            return Ok(await _jobPostingService.CreateJobPosting(create).ConfigureAwait(false));
        }

        /// <summary>
        /// Agrega preguntas a una vacante existente.
        /// </summary>
        /// <remarks>
        /// Permite a un empleador agregar o actualizar preguntas de aplicación en un Job Posting previamente creado.
        /// </remarks>
        /// <param name="questions">Lista de preguntas a agregar.</param>
        /// <returns>Respuesta con la vacante actualizada.</returns>
        /// <response code="500">InternalServerError. Ha ocurrido una excepción no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("CreateQuestions")]
        [ProducesResponseType(typeof(Response<GetJobPostingDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateQuestions([FromBody] List<CreateApplicationQuestionDtoRequest> questions)
        {
            return Ok(await _jobPostingService.CreateQuestions(questions).ConfigureAwait(false));
        }

        /// <summary>
        /// Obtiene una vacante por su Id.
        /// </summary>
        /// <remarks>
        /// Devuelve los detalles completos de un Job Posting incluyendo preguntas de aplicación si existen.
        /// </remarks>
        /// <param name="jobId">Id de la vacante a consultar.</param>
        /// <returns>Respuesta con los datos de la vacante.</returns>
        /// <response code="500">InternalServerError. Ha ocurrido una excepción no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("GetJobPostingById")]
        [ProducesResponseType(typeof(Response<GetJobPostingDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobPostingById([Required][FromHeader]int jobId)
        {
            return Ok(await _jobPostingService.GetJobPostingById(jobId).ConfigureAwait(false));
        }

        /// <summary>
        /// Obtiene una lista de vacantes según filtros y paginación.
        /// </summary>
        /// <remarks>
        /// Permite filtrar vacantes por ubicación, experiencia mínima, nivel educativo, palabras clave y paginarlas.
        /// </remarks>
        /// <param name="filter">Objeto con los filtros de búsqueda y parámetros de paginación.</param>
        /// <returns>Respuesta con la lista de vacantes paginadas.</returns>
        /// <response code="500">InternalServerError. Ha ocurrido una excepción no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("GetJobPostings")]
        [ProducesResponseType(typeof(Response<PaginationResponse<GetJobPostingDtoResponse?>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobPostings([FromBody] JobPostingQueryFilter filter)
        {
            return Ok(await _jobPostingService.GetJobPostings(filter).ConfigureAwait(false));
        }
    }
}