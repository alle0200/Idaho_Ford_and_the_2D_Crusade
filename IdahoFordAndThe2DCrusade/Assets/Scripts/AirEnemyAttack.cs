using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyAttack : StateMachineBehaviour
{
    [SerializeField] private float chaseSpeed = 2;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool isMovingRight = animator.GetComponent<AirEnemy>().GetMovingRight();
        Transform playerLocation = animator.GetComponent<AirEnemy>().GetPlayerPosition();

        // Debug.Log(Vector3.Lerp(animator.transform.position, playerLocation.position, Time.deltaTime * speed));
        animator.transform.position = Vector3.Lerp(animator.transform.position, playerLocation.position, Time.deltaTime * chaseSpeed);
        
        // Debug.Log(move.x - playerLocation.position.x);
        if (animator.transform.position.x > playerLocation.position.x && isMovingRight)
        {
            animator.GetComponent<AirEnemy>().TurnAround();
        }
        
        else if (animator.transform.position.x < playerLocation.position.x  && !isMovingRight)
        {
            animator.GetComponent<AirEnemy>().TurnAround();
        }
    }

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
