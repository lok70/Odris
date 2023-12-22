using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideWaitingState : GuideState
{
    public GuideWaitingState(BaseGuide _guide, GuideStateMachine _stateMachine) : base(_guide, _stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        //guide.anim.SetBool("isWaiting", true);
    }

    public override void ExitState()
    {
        base.ExitState();
        //guide.anim.SetBool("isWaiting", false);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (guide.PlayerDistance() <= 2f && !guide.obctacklesChecker())
        {
            // guideStateMachine.ChangeState(GuideDialogState);
            guide.moveEnemy(guide.player.transform.position);
        }
    }

    
}
