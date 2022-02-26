using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;

namespace Typhon.JobDriver
{
    internal class AttackPawn : Verse.AI.JobDriver_AttackMelee
    {
        public override void Notify_PatherFailed()
        {
            EndJobWith(JobCondition.ErroredPather);
        }
    }
}
