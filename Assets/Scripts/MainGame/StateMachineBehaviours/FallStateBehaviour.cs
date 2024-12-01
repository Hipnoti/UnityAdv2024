using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FallStateBehaviour : StateMachineBehaviour
{
   public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
      AnimatorControllerPlayable controller)
   {
      // base.OnStateEnter(animator, stateInfo, layerIndex, controller);
      // //DEAR GOD GET COMPONENT. Hanz, get the flamethrower.
      // animator.GetComponent<PlayerCharacterController>().ShowFallEffect();
   }
}
