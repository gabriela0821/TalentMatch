using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentMatch.Core.DTOs.JobMatch.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Response;
using TalentMatch.Core.Interfaces.Services;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.QueryFilters;

namespace TalentMatch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MatchingServiceController : ControllerBase
    {
        #region Attributes

        private readonly IMatchingService _matchingService;

        #endregion Attributes

        #region Builder

        public MatchingServiceController(IMatchingService matchingService)
        {
            _matchingService = matchingService;
        }

        #endregion Builder

        /// <summary>
        /// Crea un registro de coincidencia (match) entre una oferta y un candidato.
        /// </summary>
        /// <remarks>
        /// Este endpoint calcula el porcentaje de compatibilidad entre un JobSeeker y un JobPosting,
        /// genera los puntajes de Skills, Experiencia, Educación y Ubicación, y almacena el resultado
        /// en la tabla JobMatches.
        /// </remarks>
        /// <param name="create">Objeto que contiene JobPostingId y JobSeekerId.</param>
        /// <returns>Respuesta indicando si el match fue creado exitosamente.</returns>
        [HttpPost("CreateMatch")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMatch([FromBody] CreateJobMatchDtoRequest create)
        {
            return Ok(await _matchingService.CreateMatch(create).ConfigureAwait(false));
        }

        /// <summary>
        /// Actualiza el estado de un match entre un candidato y una oferta.
        /// </summary>
        /// <remarks>
        /// El status puede cambiar entre: Pending, Viewed, Applied o Rejected.
        /// </remarks>
        /// <param name="update">Objeto que contiene MatchId y Status.</param>
        /// <returns>Respuesta indicando si la actualización fue exitosa.</returns>
        [HttpPost("UpdateMatchStatus")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMatchStatus(UpdateJobMatchDtoRequest update)
        {
            return Ok(await _matchingService.UpdateMatchStatus(update).ConfigureAwait(false));
        }

        /// <summary>
        /// Obtiene los matches filtrados por oferta, candidato o estado.
        /// </summary>
        /// <remarks>
        /// Permite recuperar los JobMatches generados, incluyendo el puntaje total y los puntajes individuales
        /// (Skills, Experiencia, Educación, Ubicación).
        /// </remarks>
        /// <param name="filter">Filtros de búsqueda opcionales (JobPostingId, JobSeekerId, Status).</param>
        /// <returns>Lista de coincidencias con detalles del match.</returns>
        [HttpPost("GetMatches")]
        [ProducesResponseType(typeof(Response<GetJobSeekerProfileDtoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMatches([FromBody] MatchesQueryFilter filter)
        {
            return Ok(await _matchingService.GetMatches(filter).ConfigureAwait(false));
        }
    }
}