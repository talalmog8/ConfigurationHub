using System;
using System.Collections.Generic;
using AutoMapper;
using ConfigurationHub.Core.Auth;
using ConfigurationHub.Data.Repositories;
using ConfigurationHub.Domain.Auth;
using ConfigurationHub.General.HelperModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IJwtTokenUtils _jwtTokenUtils;
        private readonly string _secret;

        public AuthenticationController(IUserService userService, IMapper mapper
            , IOptions<AppSettings> appSettings, IJwtTokenUtils jwtTokenUtils)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtTokenUtils = jwtTokenUtils;
            _secret = appSettings.Value.Secret;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateDto model)
        {
            try
            {
                User user = _userService.Authenticate(model.Username, model.Password);
                return Ok(new
                {
                    user.Id,
                    user.Username,
                    user.FirstName,
                    user.LastName,
                    Token = _jwtTokenUtils.CreateToken(_secret, user.Id.ToString())
                });

            }
            catch(ArgumentException ex)
            {
                return BadRequest(new {message = $"Couldn't Authenticate Because: {ex.Message}", FaultedParameter = ex.ParamName });
            }
            catch (InvalidOperationException)
            {
                return BadRequest(new { message = "Couldn't Authenticate User, Received Invalid Data"});
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto model)
        {
            try
            {
                _userService.Create(_mapper.Map<User>(model), model.Password);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, FaultedParameter = ex.ParamName });
            }
        }
    }
}