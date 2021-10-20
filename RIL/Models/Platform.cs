using System.ComponentModel;

namespace RIL.Models
{
    public enum Platform
    {
        [Description("Windows")]
        Windows,

        [Description("MacOS")]
        MacOs,

        [Description("Linux")]
        Linux,

        [Description("Xbox 360")]
        Xbox360,

        [Description("Xbox One")]
        XboxOne,

        [Description("Xbox Series X/S")]
        XboxSeriesXs,

        [Description("Playstation 3")]
        Playstation3,

        [Description("Playstation 4")]
        Playstation4,

        [Description("Playstation 5")]
        Playstation5,

        [Description("Nintendo Switch")]
        NintendoSwitch,

        [Description("Android")]
        Android,

        [Description("iOS")]
        IOs,
    }
}
