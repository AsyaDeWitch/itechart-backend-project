using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class ReturnProductViewModel
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
        /// Platform name
        /// </summary>
        /// <example>Windows</example>
        [Required]
        public string Platform { get; set; }

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
        /// Product genre
        /// </summary>
        /// <example>RPG</example>
        [Required]
        public string Genre { get; set; }

        /// <summary>
        /// Rating by age name
        /// </summary>
        /// <example>Rated G: General audiences – All ages admitted</example>
        [Required]
        public string Rating { get; set; }

        /// <summary>
        /// Link to logo image
        /// </summary>
        /// <example>https://firebasestorage.googleapis.com/[project_path]/[image_path]?alt=[type]</example>
        public string Logo { get; set; }

        /// <summary>
        /// Link to background image
        /// </summary>
        /// <example>https://firebasestorage.googleapis.com/[project_path]/[image_path]?alt=[type]</example>
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
    }
}
