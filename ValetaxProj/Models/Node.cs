using System.ComponentModel.DataAnnotations.Schema;

namespace ValetaxProj.Models
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Node Parent { get; set; }
        public virtual ICollection<Node> ChildNodes { get; set; }
    }
}
