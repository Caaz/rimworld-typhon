using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;

namespace Typhon
{
    internal class JobDriver_AttackPawn : JobDriver_AttackMelee
    {
        public override void Notify_PatherFailed()
        {
            EndJobWith(JobCondition.ErroredPather);
        }
    }
}
