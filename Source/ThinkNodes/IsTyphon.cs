using Verse;
using Verse.AI;
namespace Typhon.ThinkNodes
{
    public class IsTyphon : ThinkNode_Conditional
    {
        protected override bool Satisfied(Pawn pawn) => TyphonUtility.IsTyphon(pawn);
    }
}
