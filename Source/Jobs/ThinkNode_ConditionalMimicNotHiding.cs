using System;
using Verse;
using Verse.AI;

namespace Typhons
{
    internal class ThinkNode_ConditionalMimicNotHiding : ThinkNode_Conditional
    {
        protected override bool Satisfied(Pawn pawn)
        {
            return true;
        }
    }
}
