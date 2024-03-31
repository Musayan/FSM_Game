using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolBehaviour : StateMachineBehaviour
{
    private EnemyFunction enemy;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyFunction>();
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     
        if (enemy.CheckObstacles())
        {
            enemy.Rotate();
            animator.SetTrigger("Idle");
        }

        else if(!enemy.CheckObstacles())
        {
            enemy.MoveEnemy();
        }

        if (enemy.CheckPlayer())
        {
            animator.SetTrigger("Chase");
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Patrol");
    }



    private void CheckPlayerDetected(Animator animator)
    {
        if (enemy.CheckPlayer() && enemy.CheckObstacles())
        {
            animator.SetTrigger("Chase");
        }
    }
}
