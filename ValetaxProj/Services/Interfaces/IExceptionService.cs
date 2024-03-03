using ValetaxProj.Models;

namespace ValetaxProj.Services.Interfaces
{
    public interface IExceptionService
    {
        Task WriteException(ExceptionsLog log);
    }
}
