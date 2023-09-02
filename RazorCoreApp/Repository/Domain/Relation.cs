using System.ComponentModel.DataAnnotations;

namespace PS1_MIC_090_Core.Repository.Domain
{
    public class Relation
    {
        [Key]
        public int RelationId { get; set; }

        public required string Name { get; set; }
    }
}
