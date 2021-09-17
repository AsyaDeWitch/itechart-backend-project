using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class ProductViewModel
    {
        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>12</example>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        /// <example>The Witcher</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Platform number
        /// </summary>
        /// <remarks>Matching between name and number specified in Platform enum</remarks>
        /// <example>0</example>
        [Required]
        public int Platform { get; set; }

        /// <summary>
        /// Product creation date
        /// </summary>
        /// <example>2013-12-31T00:00:00.00Z</example>
        [Required]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Average critics rating
        /// </summary>
        /// <example>9.6</example>
        [Required]
        public double TotalRating { get; set; }
    }
}
