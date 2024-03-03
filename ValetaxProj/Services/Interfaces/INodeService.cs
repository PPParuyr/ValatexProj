using ValetaxProj.Models;

namespace ValetaxProj.Services.Interfaces
{
    public interface INodeService
    {
        Task<List<NodeViewModel>> GetAllNodes();
        Task AddNode(NodeViewModel node);
        Task UpdateNode(NodeViewModel node);
        Task DeleteNode(int id);
        Task<NodeViewModel> GetNode(int id);
    }
}
