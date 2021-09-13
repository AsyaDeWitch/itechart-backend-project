using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string HouseBuilding { get; set; }
        public int EntranceNumber { get; set; }
        public int FloorNumber { get; set; }
        public int FlatNumber { get; set; }
    }
}
