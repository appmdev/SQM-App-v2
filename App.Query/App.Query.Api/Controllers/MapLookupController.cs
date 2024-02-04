using App.Common.DTOs;
using App.Query.Api.DTOs;
using App.Query.Api.Queries;
using App.Query.Domain.Entities;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace App.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MapLookupController: ControllerBase
    {
        private readonly ILogger<MapLookupController> _logger;
        private readonly IQueryDispatcher<MapEntity> _queryDispatcher;
        public MapLookupController(ILogger<MapLookupController> logger, IQueryDispatcher<MapEntity> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllMapsAsync()
        {
            try
            {
                var maps = await _queryDispatcher.SendAsync(new FindAllMapsQuery());

                return NormalResponse(maps);

            } catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retreive all maps!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        [HttpGet("byId/{mapId}")]
        public async Task<ActionResult> GetMapByIdAsync(Guid mapId)
        {
            try
            {
                var maps = await _queryDispatcher.SendAsync(new FindMapByIdQuery { Id = mapId });

                return NormalResponse(maps);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retreive map by ID!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }
        private ActionResult NormalResponse(List<MapEntity> maps)
        {
            if (maps == null || !maps.Any())
            {
                return NoContent();
            }

            var count = maps.Count;
            return Ok(new MapLookupResponse
            {
                Maps = maps,
                Message = $"Successfuly returned {count} map{(count > 1 ? "s" : string.Empty)}!"
            });
        }
        private ActionResult ErrorResponse(Exception ex, string safeErrorMessage)
        {
            _logger.LogError(safeErrorMessage, ex);
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = safeErrorMessage,
            });
        }
    }
}
