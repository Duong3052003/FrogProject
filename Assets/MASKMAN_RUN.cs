using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MASKMAN_RUN : StateMachineBehaviour
{
    private Transform Player;
    [SerializeField] private float speed;
    [SerializeField] private float timeMin;
    [SerializeField] private float timeMax;
    private float time;

    private GameObject MASKMAN;
    private BouncWall BouncWall;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        MASKMAN = GameObject.FindGameObjectWithTag("BOSS");
        BouncWall = MASKMAN.GetComponent<BouncWall>();
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
            BouncWall.InteractWall();
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
