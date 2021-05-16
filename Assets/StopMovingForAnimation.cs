using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovingForAnimation : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //stop running
        Vector3 maxDistanceDelta = Vector3.MoveTowards(animator.GetComponent<AIPlayer>().transform.position, animator.GetComponent<AIPlayer>().transform.position ,0);
        animator.GetComponent<AIPlayer>().transform.position = new Vector3(maxDistanceDelta.x,0,maxDistanceDelta.z);
        
    //    GetComponent(AIPlayer).moveTowards(transform.position,transform.position,0);
    }
    
    private IEnumerator stopMovingTimer()
    {
        Debug.Log("waiting for 7 seconds");
        yield return new WaitForSecondsRealtime(7);
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.GetComponent<AIPlayer>().StartCoroutine(stopMovingTimer());
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //start running again
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
