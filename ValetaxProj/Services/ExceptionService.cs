using ValetaxProj.Models;
using ValetaxProj.Services.Interfaces;

namespace ValetaxProj.Services
{
    public class ExceptionService : IExceptionService
    {
        private readonly MyDbContext _context;
        public ExceptionService(MyDbContext context)
        {
            _context = context;
        }
        public async Task WriteException(ExceptionsLog log)
        {
            await _context.Exceptions.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
