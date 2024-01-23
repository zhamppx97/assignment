using api.Core;
using api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    private readonly IUsersService _usersService = usersService;

    [HttpGet]
    [Route("select")]
    [Produces("application/json")]
    public async Task<ActionResult<UserServiceResponse<List<UsersResponse>>>> SelectAsync()
    {
        try
        {
            return Ok(await _usersService.SelectAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("insert")]
    [Produces("application/json")]
    public async Task<ActionResult<UserServiceResponse<Users>>> InsertAsync([FromBody] UserServiceRequest req)
    {
        try
        {
            if (ModelState.IsValid)
            {
                return Ok(await _usersService.InsertAsync(req));
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    [Route("update")]
    [Produces("application/json")]
    public async Task<ActionResult<UserServiceResponse<Users>>> UpdateAsync([FromBody] UserServiceRequest req)
    {
        try
        {
            if (ModelState.IsValid)
            {
                return Ok(await _usersService.UpdateAsync(req));
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    [Route("delete")]
    [Produces("application/json")]
    public async Task<ActionResult<UserServiceResponse<Users>>> DeleteAsync(Guid userId)
    {
        try
        {
            return Ok(await _usersService.DeleteAsync(userId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
