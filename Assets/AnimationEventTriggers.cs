using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventTriggers : StateMachineBehaviour
{
    [SerializeField][Tooltip("Triggers on Enter if false")]
    bool _triggersOnExit;

    [SerializeField]
    Kaideu.Events.Events evt;

    [SerializeField][Tooltip("If applicable")]
    RagdollController.RagdollAnimState animationState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!_triggersOnExit)
            Kaideu.Events.EventManager.Instance.TriggerEvent(evt, new Dictionary<string, object> { { "State", animationState} });
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_triggersOnExit)
            Kaideu.Events.EventManager.Instance.TriggerEvent(evt, new Dictionary<string, object> { { "State", animationState } });
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}


}
