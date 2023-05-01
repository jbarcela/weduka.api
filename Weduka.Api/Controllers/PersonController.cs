using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weduka.Application.Commands.Person.Create;
using Weduka.Application.Commands.Person.Delete;
using Weduka.Application.Commands.Person.Update;
using Weduka.Application.Exceptions;
using Weduka.Application.Queries;
using Weduka.Application.Queries.Person.GetById;

namespace Weduka.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var request = new GetAllPersonQueryRequest();
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson([FromRoute] int id)
    {
        try
        {
            var request = new GetByIdPersonQueryRequest(id);
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommandRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);
            
            if (response == null)
            {
                return BadRequest();
            }
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonCommandRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson([FromRoute] int id)
    {
        try
        {
            var request = new DeletePersonCommandRequest(id);
            var response = await _mediator.Send(request);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

}