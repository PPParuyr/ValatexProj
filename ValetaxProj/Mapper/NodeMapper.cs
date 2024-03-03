using AutoMapper;
using ValetaxProj.Models;

namespace ValetaxProj.Mapper
{
    internal class NodeMapperProfile : Profile
    {
        public NodeMapperProfile()
        {
            CreateMap<Node, NodeViewModel>().ReverseMap();
        }
    }
}
