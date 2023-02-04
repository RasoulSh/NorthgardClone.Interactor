using Northgard.Interactor.Enums.WorldEnums;

namespace Northgard.Interactor.Common.WorldUtilities
{
    public static class WorldDirectionUtil
    {
        public static WorldDirection OpposeDirection(WorldDirection direction)
        {
            return direction switch
            {
                WorldDirection.East => WorldDirection.West,
                WorldDirection.West => WorldDirection.East,
                WorldDirection.North => WorldDirection.South,
                WorldDirection.South => WorldDirection.North,
                _ => default
            };
        }
    }
}