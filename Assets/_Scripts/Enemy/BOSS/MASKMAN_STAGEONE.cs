using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MASKMAN_STAGEONE : StateMachineBehaviour
{
    private float ranged;
    [SerializeField] private float time;
    private float eslapedTime = 0;
    private bool isChanged;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isChanged=false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (eslapedTime < time)
        {
            eslapedTime += Time.deltaTime;
        }
        else
        {
            if (isChanged == false)
            {
                ranged = Random.Range(0, 2);
                isChanged = true;
            }
            if (ranged == 0)
            {
                animator.SetTrigger("Jump");
            }
            else
            {
                animator.SetTrigger("Run");
            }
        }

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
