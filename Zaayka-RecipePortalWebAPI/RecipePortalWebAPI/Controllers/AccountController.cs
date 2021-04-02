using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePortalWebAPI.Data;
using RecipePortalWebAPI.Models;

namespace RecipePortalWebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string msg=null;
            try
            {
                if (user != null)
                {
                    msg = _accountRepository.Register(user);
                }
                else
                {
                    return NotFound(StatusCodes.Status404NotFound);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding User: "+ex.Message);
            }
            return Ok(msg);
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var get_user = _accountRepository.Login(user);
            if(get_user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Username or Password");
            }
            return Ok(get_user);
        }
        [Route("getuserdetails/{id}")]
        [HttpGet]
        public IActionResult GetUserDetails(int id)
        {
            var get_user = _accountRepository.GetUser(id);
            if (get_user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid Userid");
            }
            return Ok(get_user);
        }
        [Route("updateuserdetails")]
        [HttpPut]
        public IActionResult UpdateUserDetails([FromBody] User user)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest();
            }*/
            var response = _accountRepository.UpdateUser(user);
            if (response == "success")
            {
                return Ok(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
