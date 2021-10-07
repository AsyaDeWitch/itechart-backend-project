using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.ViewModels
{
    public class ProductViewModel
    {
        /// <summary>
        /// Product Id
        /// </summary>
        /// <example>12</example>
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

        /// <summary>
        /// Product genre number
        /// </summary>
        /// <example>0</example>
        [Required]
        public int Genre { get; set; }

        /// <summary>
        /// Rating by age
        /// </summary>
        /// <remarks>Matching between string description rating and number specified in Rating enum</remarks>
        /// <example>0</example>
        [Required]
        public int Rating { get; set; }

        /// <summary>
        /// Link to logo image
        /// </summary>
        /// <example>https://firebasestorage.googleapis.com/[project_path]/[image_path]?alt=[type]&token=[token_string]</example>
        public string Logo { get; set; }

        /// <summary>
        /// Link to background image
        /// </summary>
        /// <example>https://firebasestorage.googleapis.com/[project_path]/[image_path]?alt=[type]&token=[token_string]</example>
        public string Background { get; set; }

        /// <summary>
        /// Product path
        /// </summary>
        /// <example>39.99</example>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Remained products count
        /// </summary>
        /// <example>2</example>
        [Required]
        public int Count { get; set; }

        /// <summary>
        /// Field for soft deletion flag
        /// </summary>
        /// <example>false</example>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Logo image file
        /// </summary>
        [NotMapped]
        public IFormFile LogoImageFile { get; set; }

        /// <summary>
        /// Background image file
        /// </summary>
        [NotMapped]
        public IFormFile BackgroundImageFile { get; set; }
    }
}
