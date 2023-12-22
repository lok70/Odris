using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideState : MonoBehaviour
{
    protected BaseGuide guide;
    protected GuideStateMachine guideStateMachine;
    public GuideState(BaseGuide _guide, GuideStateMachine _stateMachine)
    {
        this.guide = _guide;
        this.guideStateMachine = _stateMachine;
    }

    public virtual void EnterState()
    {
    }

    public virtual void ExitState() { }

    public virtual void FrameUpdate() { }
     
    public virtual void FixedUpdate() { }
}
