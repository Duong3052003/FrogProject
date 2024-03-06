using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MASKMAN_JUMP : StateMachineBehaviour
{
    private GameObject _MASKMAN;
    private MASKMAN MASKMAN;
    [SerializeField] private float timeMin;
    [SerializeField] private float timeMax;
    [SerializeField] private Vector2 jumpVector;
    private float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _MASKMAN = GameObject.FindGameObjectWithTag("BOSS");
        MASKMAN = _MASKMAN.GetComponent<MASKMAN>();
        time = Random.Range(timeMin, timeMax);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (time <= 0)
        {
            animator.SetTrigger("Idle");
        }

        if (time > 0)
        {
            MASKMAN.Jump(jumpVector.x, jumpVector.y);
            time -= Time.deltaTime;
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
