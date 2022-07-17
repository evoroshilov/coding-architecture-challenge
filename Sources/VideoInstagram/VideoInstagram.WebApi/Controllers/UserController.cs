using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoInstagram.DataLayer.Repositories;
using VideoInstagram.Shared.Contracts;
using VideoInstagram.WebApi.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoInstagram.WebApi.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserController(IUserRepository UserRepository, IMapper mapper)
        {
            _userRepository = UserRepository;
            _mapper = mapper;
        }

        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userDto, CancellationToken cancellationToken)
        {

            await _userRepository.CreateUserAsync(_mapper.Map<User>(userDto), cancellationToken);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<UserDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken)
        {
            //use cache 20/80 ..redis with ttl 2 min
            var list = await _userRepository.GetAllUsersAsync(cancellationToken);

            return Ok(list.Select(_mapper.Map<List<UserDto>>));
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(IReadOnlyList<UserDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserAsync([FromRoute][Range(1, int.MaxValue)] int id, CancellationToken cancellationToken)
        {
            //use cache 20/80 ..redis with ttl 2 min
            var user = await _userRepository.GetUserAsync(id, cancellationToken);
            return Ok(user);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute][Range(1, int.MaxValue)] int id, [FromBody] UserDto UserDto, CancellationToken cancellationToken)
        {
            if (id != UserDto.Id)
            {
                return BadRequest("ID in Url and body must match.");
            }
            await _userRepository.UpdateUserAsync(_mapper.Map<User>(UserDto), cancellationToken);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUsersAsync([FromRoute][Range(1, int.MaxValue)] int id, CancellationToken cancellationToken)
        {
            await _userRepository.RemoveUserAsync(id, cancellationToken);

            return Ok();
        }
    }
}
