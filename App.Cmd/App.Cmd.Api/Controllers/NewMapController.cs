using App.Cmd.Api.Commands;
using App.Cmd.Api.DTOs;
using App.Common.DTOs;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace App.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewMapController : ControllerBase
    {
        private readonly ILogger<NewMapController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public NewMapController(ILogger<NewMapController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> NewMapAsync(NewMapCommand command)
        {
            var id = Guid.NewGuid();
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status201Created, new NewMapResponse
                {
                    Id = id,
                    Message = "New map creation request ccompletedd successfuly!"
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Client made a bad request");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new map!";

                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new NewMapResponse
                {
                    Id = id,
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}