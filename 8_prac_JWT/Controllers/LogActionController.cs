using _8_prac_JWT.CustomAttributes;
using _8_prac_JWT.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Controllers
{
    public class LogActionController : ControllerBase
    {
        private readonly ILogActionInterfaces _logActionService;

        public LogActionController(ILogActionInterfaces logActionService)
        {
            _logActionService = logActionService;
        }

        [HttpGet]
        [Route("GetAllLogUserActions")]
        [RoleAuthorized([1])]
        public async Task<IActionResult> GetAllLogUserActionsAsync()
        {
            return await _logActionService.GetAllLogUserActionsAsync();
        }
    }
}
