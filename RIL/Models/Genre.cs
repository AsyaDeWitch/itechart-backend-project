using System.ComponentModel;

namespace RIL.Models
{
    public enum Genre
    {
        [Description("Sandbox")]
        Sandbox,

        [Description("Real-time strategy")]
        RTS,

        [Description("Shooter")]
        Shooter,

        [Description("Multiplayer online battle arena")]
        MOBA,

        [Description("Role-playing")]
        RPG,

        [Description("Simulation")]
        Simulation,

        [Description("Sports")]
        Sports,

        [Description("Puzzler")]
        Puzzler,

        [Description("Party game")]
        Party,

        [Description("Action-adventure")]
        Action_adventure,

        [Description("Survival")]
        Survival,

        [Description("Horror")]
        Horror,

        [Description("Platformer")]
        Platformer,

        [Description("Turn-based strategy with RPG elements")]
        TBS_with_RPG,
    }
}
