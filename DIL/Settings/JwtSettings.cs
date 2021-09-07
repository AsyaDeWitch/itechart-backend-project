using System.ComponentModel.DataAnnotations;

namespace DIL.Settings
{
    public class JwtSettings
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Audience { get; set; }
    }
}
