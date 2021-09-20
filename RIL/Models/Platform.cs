using System.ComponentModel;

namespace RIL.Models
{
    public enum Platform
    {
        [Description("Windows")]
        Windows,

        [Description("MacOS")]
        MacOS,

        [Description("Linux")]
        Linux,

        [Description("Xbox 360")]
        Xbox_360,

        [Description("Xbox One")]
        Xbox_One,

        [Description("Xbox Series X/S")]
        Xbox_Series_XS,

        [Description("Playstation 3")]
        Playstation_3,

        [Description("Playstation 4")]
        Playstation_4,

        [Description("Playstation 5")]
        Playstation_5,

        [Description("Nintendo Switch")]
        Nintendo_Switch,

        [Description("Android")]
        Android,

        [Description("iOS")]
        iOS,
    }
}
