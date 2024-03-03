using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using ValetaxProj.Exceptions;
using ValetaxProj.Models;
using ValetaxProj.Services.Interfaces;

namespace ValetaxProj.Services
{
    public class NodeService : INodeService
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public NodeService(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddNode(NodeViewModel model)
        {
            Node node = new()
            {
                Name = model.Name,
                ParentId = model.ParentId,
            };
            await _context.Nodes.AddAsync(node);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNode(int id)
        {
            try
            {
                var node = await _context.Nodes.FindAsync(id);

                _context.Nodes.Remove(node);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message, ex.StackTrace);
            }
        }

        public async Task<List<NodeViewModel>> GetAllNodes()
        {
            List<Node> model = await _context.Nodes
                .Include(x => x.ChildNodes)
                .ToListAsync();

            List<NodeViewModel> nodes = _mapper.Map<List<NodeViewModel>>(model)
                .Where(x => !x.ParentId.HasValue)
                .ToList();

            return nodes;
        }

        public async Task<NodeViewModel> GetNode(int id)
        {
            Node node = await _context.Nodes
                .Include(x=>x.ChildNodes)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<NodeViewModel>(node);
        }

        public async Task UpdateNode(NodeViewModel model)
        {
            try
            {
                Node node = await _context.Nodes.FindAsync(model.Id);

                node.Name = model.Name;
                node.ParentId = model.ParentId;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new SecureException(ex.Message,ex.StackTrace);
            }
           
        }
    }
}
