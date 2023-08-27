using System.ComponentModel.DataAnnotations;

namespace PS1_MIC_090_Core.Repository.Domain
{
    public class NameSuffix
    {
        [Key]
        public int SuffixId { get; set; }

        public required string Suffix { get; set; }
    }
}
