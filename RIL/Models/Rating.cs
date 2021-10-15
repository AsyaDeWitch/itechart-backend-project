using System.ComponentModel;

namespace RIL.Models
{
    public enum Rating
    {
        [Description("Rated G: General audiences – All ages admitted")]
        G,

        [Description("Rated PG: Parental guidance suggested – Some material may not be suitable for children")]
        Pg,

        [Description("Rated PG-13: Parents strongly cautioned – Some material may be inappropriate for children under 13")]
        Pg13,

        [Description("Rated R: Restricted – Under 17 requires accompanying parent or adult guardian")]
        R,

        [Description("Rated NC-17: Adults Only – No one 17 and under admitted")]
        Nc17,
    }
}
