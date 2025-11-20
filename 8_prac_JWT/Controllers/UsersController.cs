using _8_prac_JWT.CustomAttributes;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Requests;
using _8_prac_JWT.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Controllers
{
    public class UsersController : ControllerBase
    {
        public readonly IUserInterfaces _userService;

        public UsersController(IUserInterfaces userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("CreateNewUser")]
        public async Task<IActionResult> CreateNewUserAsync([FromBody] CreateNewUser createNewUser)
        {
            return await _userService.CreateNewUserAsync(createNewUser);
        }

        [HttpPost]
        [Route("AuthUser")]
        
        public async Task<IActionResult> AuthUserAsync([FromBody]AuthUserRequest authUserRequest)
        {
            return await _userService.AuthUserAsync(authUserRequest);
        }

        [HttpPost]
        [Route("CreateNewEmployee")]
        [RoleAuthorized([1])]
        public async Task<IActionResult> CreateNewEmployeeAsync([FromBody]CreateNewEmployee createNewEmployee)
        {
            return await _userService.CreateNewEmployeeAsync(createNewEmployee);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        [RoleAuthorized([1, 2])]
        public async Task<IActionResult> DeleteUserAsync([FromBody]DeletedIDUserRequest deletedID)
        {
            return await _userService.DeleteUserAsync(deletedID);
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        [RoleAuthorized([1])]
        public async Task<IActionResult> DeleteEmployeeAsync([FromBody] DeletedIDEmployeeRequest deletedIDRequest_)
        {
            return await _userService.DeleteEmployeeAsync(deletedIDRequest_);
        }

        [HttpGet]
        [Route("GetAllUser")]
        [RoleAuthorized([1,2])]
        public async Task<IActionResult> GetAllUserAsync( )
        {
            return await _userService.GetAllUserAsync();
        }

        [HttpGet]
        [Route("GetAllEmployee")]
        [RoleAuthorized([1])]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            return await _userService.GetAllEmployeeAsync();
        }

        [HttpPut]
        [Route("UpdateUser")]
        [RoleAuthorized([1, 2])]
        public async Task<IActionResult> PutUserAsync([FromBody]PutUserRequest putUserRequest)
        {
            return await _userService.PutUserAsync(putUserRequest);
        }
    }
}
