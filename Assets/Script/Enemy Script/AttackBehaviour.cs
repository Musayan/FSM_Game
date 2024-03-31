using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    private EnemyFunction enemy;
    private float _waitTime = 1f;
    private float _waitChase = 0.5f;
   

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyFunction>();
        _waitChase = 0.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0)
        {
            if (!enemy.AttackDistance())
            {
               _waitChase -= Time.deltaTime;
                if (_waitChase < 0)
                animator.SetTrigger("Chase");
            }
            
            _waitTime = 1f;
        }

       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


    
}
