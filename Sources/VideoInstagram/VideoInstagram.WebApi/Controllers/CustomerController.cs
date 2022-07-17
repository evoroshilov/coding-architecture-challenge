using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoInstagram.DataLayer.Repositories;
using VideoInstagram.Shared.Contracts;
using VideoInstagram.WebApi.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoInstagram.WebApi.Controllers
{
    [Route("api/v1/cutomer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
     

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
        {
           
            await _customerRepository.CreateCustomerAsync(_mapper.Map<Customer>(customerDto), cancellationToken);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<CustomerDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCustomersAsync(CancellationToken cancellationToken)
        {
            //use cache 20/80
            var list = await _customerRepository.GetAllCustomersAsync(cancellationToken);
          
            return Ok(list.Select(_mapper.Map<List<CustomerDto>>));
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(IReadOnlyList<CustomerDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCustomerAsync([FromRoute][Range(1, int.MaxValue)] int id, CancellationToken cancellationToken)
        {
            //use cache 20/80
            var customer = await _customerRepository.GetCustomerAsync(id, cancellationToken);
            return Ok(customer);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCustomerAsync([FromRoute][Range(1, int.MaxValue)] int id, [FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
        {
            if (id != customerDto.Id)
            {
                return BadRequest("ID in Url and body must match.");
            }
            await _customerRepository.UpdateCustomerAsync(_mapper.Map<Customer>(customerDto), cancellationToken);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCustomersAsync([FromRoute][Range(1, int.MaxValue)] int id, CancellationToken cancellationToken)
        {
            await _customerRepository.RemoveCustomerAsync(id,cancellationToken);

            return Ok();
        }
    }
}
