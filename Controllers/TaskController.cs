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

        // Method to validate string input(name)
        private static bool NameValidator(string nameValue)
        {
            var regEx = @"^[a-zA-Z- ]*$";
            if(Regex.IsMatch(nameValue, regEx, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        public class Person
        {
            public string Name { get; set; }
        }

                
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]  Person personItem)
        {
            //Validate the name (Private method defined for this operation is above)
            if(NameValidator(personItem.Name))
            {
                var instance = await _service.Create(personItem.Name);
                if(instance != null)
                {
                    return CreatedAtAction(nameof(GetUser), new{id = instance.Id}, instance);
                }

                // If Person item not created (Db issue) return code => 500

                return StatusCode(500, "Internal server error");
            }

            // Status code 400 (client error) => name failed to pass defined string validation
            
            return StatusCode(400, $"The name {personItem.Name} is not valid");
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetUser(Guid id)
        {
            if(id != Guid.Empty)
            {
                var userInstance = await _service.View(id);
                if(userInstance != null)
                {
                    return userInstance;
                }

                return NotFound();
            }

            // Status code 400 (client error) => client failed to pass in id             
            
            return StatusCode(400, $"The id: {id} is not valid");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(Guid id, UserViewModel viewModel)
        {
            if(id != Guid.Empty && ModelState.IsValid)
            {
                var isUpdated = await _service.Update(id, viewModel);
                if(isUpdated)
                {
                    return NoContent();
                }

                return NotFound();
            }

            // Status code 400 (client error) => name failed to pass in id
        
            return StatusCode(400, $"The id: {id} is not valid");
        }

        [HttpDelete("{id}")]
        public async  Task<IActionResult> DeleteUser(Guid id)
        {
            if(id != Guid.Empty)
            {
                var isDeleted = await _service.Delete(id);
                if(isDeleted)
                {
                    return NoContent();
                }

                return NotFound();
            }

            // Status code 400 (client error) => name failed to pass in id

            return StatusCode(400, $"The id: {id} is not valid");
        }
    }
}