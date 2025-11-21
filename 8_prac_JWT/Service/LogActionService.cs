using _8_prac_JWT.DatabaseContext;
using _8_prac_JWT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_prac_JWT.Service
{
    public class LogActionService : ILogActionInterfaces
    {
        private readonly ContextDb _context;

        public LogActionService(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAllLogUserActionsAsync()
        {
            var logs = await _context.LogUsers.Include(l => l.Action).ToListAsync();


            if(logs.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    message = "Информация не найдены",
                    status = false
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                data = new { logs = logs }
            });
        }
    }
}
