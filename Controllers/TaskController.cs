using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Services;
using src.ViewModels;

namespace src.Controllers{
    [ApiController]
    [Route("api/[action]")]
    public class TaskController : ControllerBase{

        private readonly IUser _service;

        public TaskController(IUser service)
        {
            _service = service;
        }

        private static bool NameValidator(string nameValue)
        {
            var regEx = @"^[a-zA-Z- ]*$";
            if(Regex.IsMatch(nameValue, regEx, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddUser(string name)
        {
            if(NameValidator(name))
            {
                var instance = await _service.Create(name);
                if(instance != null)
                {
                    return CreatedAtAction(nameof(GetUser), new{name = instance.Name}, instance);
                }

                return StatusCode(500, "Internal server error");
            }
            
            return StatusCode(400, "Name is not valid");
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<UserInfo>> GetUser(string name)
        {
            if(NameValidator(name))
            {
                var userInstance = await _service.View(name);
                if(userInstance != null)
                {
                    return userInstance;
                }

                return NotFound();
            }
            
            
            return StatusCode(400, "Name is not valid");
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> EditUser(string name, UserViewModel viewModel)
        {
            if(NameValidator(name) && ModelState.IsValid)
            {
                var instance = await _service.Update(name, viewModel);
                if(instance)
                {
                    return NoContent();
                }

                return NotFound();
            }
        
            return StatusCode(400, "Input is not valid");
        }

        [HttpDelete("{name}")]
        public async  Task<IActionResult> DeleteUser(string name)
        {
            if(NameValidator(name))
            {
                var instance = await _service.Delete(name);
                if(instance)
                {
                    return NoContent();
                }

                return NotFound();
            }

            return StatusCode(400, "Name is not valid");
        }
    }
}