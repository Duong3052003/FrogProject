using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MASKMAN_TELEPORT : StateMachineBehaviour
{
    private GameObject _MASKMAN;
    private MASKMAN MASKMAN;
    private float time;
    [SerializeField] private float timeMin;
    [SerializeField] private float timeMax;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _MASKMAN = GameObject.FindGameObjectWithTag("BOSS");
        MASKMAN = _MASKMAN.GetComponent<MASKMAN>();
        MASKMAN.Teleport();
        time = Random.Range(timeMin, timeMax);

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (time <= 0)
        {
            animator.SetTrigger("Teleport");
        }

        if (time > 0)
        {
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
