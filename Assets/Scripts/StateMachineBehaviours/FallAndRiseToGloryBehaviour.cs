using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAndRiseToGloryBehaviour : StateMachineBehaviour
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);
        //DEAR GOD GET COMPONENT. Hanz, get the flamethrower.
        animator.GetComponent<CharacterController>().ShowFallEffect();
        
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash);
        //DEAR GOD GET COMPONENT. Hanz, get the flamethrower.
        animator.GetComponent<CharacterController>().ToggleMoving(true);
    }
    
}
