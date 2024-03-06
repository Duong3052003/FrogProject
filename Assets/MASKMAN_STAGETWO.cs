using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MASKMAN_STAGETWO : StateMachineBehaviour
{
    [SerializeField] private int rangedMin;
    [SerializeField] private int rangedMax;

    private int ranged;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ranged = Random.Range(rangedMin, rangedMax);

        if (ranged == 0)
        {
            animator.SetBool("IsJumping", true);
        }
        if (ranged ==1)
        {
            animator.SetTrigger("Run");
        }
        if (ranged == 2)
        {
            animator.SetTrigger("Teleport");
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

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
