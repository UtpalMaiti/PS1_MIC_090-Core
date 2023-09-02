using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.OpenApi;

namespace BlazorApp.Server.Models
{
    public class NameSuffix
    {
        [Key]
        public int SuffixId { get; set; }

        public required string Suffix { get; set; }
    }
}