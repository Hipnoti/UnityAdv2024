using UnityEngine;

public class ParticlePlayStateBehaviour : StateMachineBehaviour
{
    [SerializeField] private float percentageToPlay;
    PlayerCharacterController playerCharacterController;
    private bool playedEffect = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        playedEffect = false;
        if(!playerCharacterController)
            playerCharacterController = animator.GetComponent<PlayerCharacterController>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!playedEffect && stateInfo.normalizedTime >= percentageToPlay)
        {
            playerCharacterController.PlaySlipEffect();
            playedEffect = true;
        }
    }
}
