using _8_prac_JWT.Requests;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Interfaces
{
    public interface IUserInterfaces
    {
        Task<IActionResult> CreateNewUserAsync(CreateNewUser createNewUser); // Создание пользователя
        Task<IActionResult> AuthUserAsync(AuthUserRequest authUserRequest);
        Task<IActionResult> CreateNewEmployeeAsync(CreateNewEmployee createNewEmployee); // Создание менеджера
        Task<IActionResult> DeleteUserAsync(DeletedIDUserRequest deletedID);
        Task<IActionResult> DeleteEmployeeAsync(DeletedIDEmployeeRequest deletedIDRequest);
        Task<IActionResult> GetAllUserAsync( );
        Task<IActionResult> GetAllEmployeeAsync( );
        Task<IActionResult> PutUserAsync(PutUserRequest putUserRequest);
        Task<IActionResult> PutEmployeeAsync(PutEmployeeRequest putEmployeeRequest);
        Task<IActionResult> PutMyProfileEmployeeAsync(PutEmployeeMyProfileRequests putEmployeeMyProfile);
        Task<IActionResult> PutUserMyProfileAsync(PutUserMyProfilesRequests putUserMyProfile);
        Task<IActionResult> PutMyProfileAdminAsync(PutAdminMyProfilesRequests putAdminMyProfiles);
        Task<IActionResult> PutUserRoleAsync(PutUserRoleRequest putUserRole);

    }
}
