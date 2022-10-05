using System.Runtime.CompilerServices;

namespace Monopoly.Shared.Enums
{
    public class DiceRoll
    {
        public int DieRoll1 { get; set; }
        public int DieRoll2 { get; set; }

        public bool DidRolledDoubles()
        {
            return DieRoll1 == DieRoll2;
        }
    }
}