using DAL.DTO.Req;
using DAL.DTO.Res;
using DAL.Repositories.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BE_PeerLending.Controllers
{
    [Route("rest/v1/user/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userservices;

        public UserController(IUserServices userServices)
        {
            _userservices = userServices;
        }
        [HttpPost]
        public async Task<IActionResult> Register(ReqRegisterUserDto register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Any())
                        .Select(x => new
                        {
                            Field = x.Key,
                            Messages = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
                        }).ToList();

                    var errorMessage = new StringBuilder("Validation error occured!");

                    return BadRequest(new ResBaseDto<object>
                    {
                        Success = false,
                        Message = errorMessage.ToString(),
                        Data = errors
                    });
                }

                var res = await _userservices.Register(register);

                return Ok(new ResBaseDto<string>
                {
                    Success = true,
                    Message = "User registered successfully",
                    Data = res
                });
            }
            catch (Exception ex)
            {
                if(ex.Message == "Email already used")
                {
                    return BadRequest(new ResBaseDto<object>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ResBaseDto<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
    }
}
