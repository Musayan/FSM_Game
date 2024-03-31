using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFunction : MonoBehaviour
{
   
    [SerializeField] private Animator _anim;

    [Header("Player")]
    public Rigidbody2D _rbEny;


    [Header("Patrol Point")]
    public float _enemyDamge = 5f;
    public int facingRight = 1;
    public Transform[] _patrolPoint;
    public float _walkSpeed = 1f;
    public float _chaseSpeed = 3f;
    public float _minDistance = 1f;
    public float _waitNextPoint = 5f;
    public float _attackCoolDown;
    [SerializeField] private float _raycastDist, _objDistance, _playerDist, _attackDist;
    [SerializeField] private LayerMask _ledge, _obstacle, _player;
    [SerializeField] private Transform _ledgeDetect;
    [SerializeField] private Transform _playerPos;
    [SerializeField] private PlayerHealth _healthPlayer;
    [SerializeField] private EnemyHealth _healthEnemy;

    public AnimationClip _hurtAnim;
      
    
    private void Start()
    {
        facingRight = 1;
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        _healthPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        
    }


    #region Patrolling 
    public void MoveEnemy()
    {
        // Calculate movement direction based on current rotation
        Vector2 movementDirection = transform.right;

        // Move forward in the direction it's facing
        _rbEny.velocity = movementDirection * _walkSpeed * Time.deltaTime;
    }

    #endregion

    #region Chase

    public void ChargePlayer()
    {
        Vector2 movementDirection = transform.right;
        _rbEny.velocity = movementDirection * _chaseSpeed;   
    }

    #endregion
                                                                                                             
    #region attack
    public bool AttackDistance()
    {
        Vector2 directToPlayer = _playerPos.position - transform.position;
        float distanceToPlayer = directToPlayer.magnitude;

        if (distanceToPlayer < _minDistance)
        {
            _rbEny.velocity = Vector2.zero;
            return true;
        }
        else
            return false;
    }


    public void attackHitCheck()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(_ledgeDetect.position, (facingRight == 1 ? Vector2.right : Vector2.left), _attackDist, _player);
           
        if (hitPlayer.collider != null)
        {
            _healthPlayer.DamagePlayer(_enemyDamge);     
        }
    }

    public void attackFinish()
    {
        _anim.SetTrigger("AttackDelay");
        _attackCoolDown = 2f;
    }
    #endregion

    #region Check if Dead

    public bool CheckIfDead()
    {
        if (_healthEnemy._enemyHealth < 0)
            return true;
        else 
            return false;
    }

    #endregion

    #region Other Script


    public void Rotate()
    {
        // Rotate 180 degrees on the Y-axis
        transform.Rotate(0, 180, 0);
        facingRight = -facingRight;

    }


    public bool CheckPlayer()
    {
       
        RaycastHit2D hitPlayer = Physics2D.Raycast(_ledgeDetect.position, (facingRight == 1 ? Vector2.right : Vector2.left), _playerDist, _player);

        if (hitPlayer.collider != null)
            return true;
        else
            return false;
    }

    public bool CheckObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(_ledgeDetect.position, Vector2.down, _raycastDist, _ledge);
        RaycastHit2D hitObj = Physics2D.Raycast(_ledgeDetect.position, Vector2.right, _objDistance, _obstacle);

        if (hit.collider == null || hitObj.collider != null)
           
            return true;
        
        else
            return false;
        
    }


    /*private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_ledgeDetect.position, (facingRight == 1 ? Vector2.right : Vector2.left) * _playerDist);
    }*/


    #endregion
}
