using Microsoft.AspNetCore.Mvc;
using user.backend.application.Users;
using user.backend.application.Users.Commands.Create;
using user.backend.domain.Users.Domain;
using user.backend.domain.Users.DTO;
using user.backend.shared;

namespace user.backend.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly UserApp _userApp;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserApp userApp, IWebHostEnvironment environment, ILogger<UsersController> logger)
        {
            _userApp = userApp;
            _hostingEnvironment = environment;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List()
        {
            StatusResponse<IEnumerable<ResponseUser>> status = await this._userApp.List();

            if (!status.Success)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateUserCommand product)
        {

            StatusResponse<User> status = await this._userApp.Create(product);
            if (!status.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, status);
            }

            return StatusCode(StatusCodes.Status201Created, status.Data);
        }
    }
}