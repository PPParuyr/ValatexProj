namespace ValetaxProj.Models
{
    public class NodeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<NodeViewModel> ChildNodes { get; set; } = new List<NodeViewModel>();
    }
}
