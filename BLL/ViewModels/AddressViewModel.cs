using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class AddressViewModel
    {
        /// <summary>
        /// Address Id
        /// </summary>
        /// <remarks>It should never be entered by the user!</remarks>
        /// <example>12</example>
        public int Id { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        /// <example>Belarus</example>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        /// <example>Minsk</example>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Street name
        /// </summary>
        /// <example>Platonova street</example>
        [Required]
        public string Street { get; set; }

        /// <summary>
        /// House number
        /// </summary>
        /// <example>49</example>
        [Required]
        public int HouseNumber { get; set; }

        /// <summary>
        /// House building
        /// </summary>
        /// <example>B</example>
        public string HouseBuilding { get; set; }

        /// <summary>
        /// Entrance number
        /// </summary>
        /// <example>2</example>
        public int EntranceNumber { get; set; }

        /// <summary>
        /// Floor number
        /// </summary>
        /// <example>9</example>
        public int FloorNumber { get; set; }

        /// <summary>
        /// Flat or room number
        /// </summary>
        /// <example>919</example> 
        [Required]
        public int FlatNumber { get; set; }
    }
}
