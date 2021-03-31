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
    [Route("api/[controller]")]
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

    }
}
