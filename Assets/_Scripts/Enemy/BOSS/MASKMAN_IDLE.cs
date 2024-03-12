using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MASKMAN_IDLE : StateMachineBehaviour
{
    [SerializeField] private float timeMin;
    [SerializeField] private float timeMax;

    [SerializeField] private int rangedMin;
    [SerializeField] private int rangedMax;

    private float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = Random.Range(timeMin, timeMax);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        if(time <=0)
        {
                int ranged;
                ranged = Random.Range(rangedMin, rangedMax);

                if (ranged == 0)
                {
                    animator.SetTrigger("Jump");
                }
                if (ranged ==1)
                {
                    animator.SetTrigger("Run");
                }
                if (ranged == 2)
                {
                    animator.SetTrigger("Teleport");
                }
                if (ranged == 3)
                {
                    animator.SetTrigger("Idle");
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
