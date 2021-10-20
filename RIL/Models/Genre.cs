using System.ComponentModel;

namespace RIL.Models
{
    public enum Genre
    {
        [Description("Sandbox")]
        Sandbox,

        [Description("Real-time strategy")]
        Rts,

        [Description("Shooter")]
        Shooter,

        [Description("Multiplayer online battle arena")]
        Moba,

        [Description("Role-playing")]
        Rpg,

        [Description("Simulation")]
        Simulation,

        [Description("Sports")]
        Sports,

        [Description("Puzzler")]
        Puzzler,

        [Description("Party game")]
        Party,

        [Description("Action-adventure")]
        ActionAdventure,

        [Description("Survival")]
        Survival,

        [Description("Horror")]
        Horror,

        [Description("Platformer")]
        Platformer,

        [Description("Turn-based strategy with RPG elements")]
        TbsWithRpg,
    }
}
