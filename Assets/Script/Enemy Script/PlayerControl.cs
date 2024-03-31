using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] Animator _playerAnim;

    private Vector2 _smoothMove;
    public Vector2 _inputMove;

    public float _moveSpeed;
    public float _jumpForce;

    public Rigidbody2D _rb;
    private Vector3 _scale;

    [SerializeField] private float _attackDelay = 2f;
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _radius = 0.2f;
    [SerializeField] LayerMask _groundLayer;


    private int facingDir;

    private void Start()
    {
        _scale = transform.localScale;  
        _rb = GetComponent<Rigidbody2D>(); 
        facingDir = 1;
    }

    private void Update()
    {
        Controller();
        FlipController();
        CheckIfDead();

    }

    private void FixedUpdate()
    {
        _smoothMove = Vector2.SmoothDamp(_smoothMove, _inputMove, ref _smoothMove, 0.01f);
        _rb.velocity = new Vector2 (_smoothMove.x * _moveSpeed * Time.deltaTime, _rb.velocity.y);
    }

    #region IsDead
    public PlayerHealth _playerHealth;
    public AnimationClip _hurtAnim;

    private void CheckIfDead()
    {
        if (CheckPlayerIsDead())
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.layer = default;
            _rb.gravityScale = 0f;
        }
    }

    public bool CheckPlayerIsDead()
    {
        if (_playerHealth._currentHealth <= 0)
            return true;
        else
            return false;
    }
    #endregion

    #region attack

    [SerializeField] private float _playerdamage = 4f;
    public LayerMask _enemy;
    public Transform _attackPos;
    [SerializeField] private float _distanceAtk;
    public LayerMask _object;
    public AnimationClip _clipAttack;

    private void Attack()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(_attackPos.position, (facingDir == 1 ? Vector2.right : Vector2.left), _distanceAtk, _enemy);

        if (hitEnemy.collider != null)
        {
            EnemyHealth _enemyHealth = hitEnemy.collider.GetComponent<EnemyHealth>();
            _enemyHealth.damageEnemy(_playerdamage);
        }

        RaycastHit2D objectHit = Physics2D.Raycast(_attackPos.position, (facingDir == 1 ? Vector2.right : Vector2.left), _distanceAtk, _object);

        if (objectHit.collider != null)
        {
            Destroy(objectHit.collider.gameObject);
        }

    }

    #endregion

    #region Other Function
    private void FlipController()
    {
        if (_inputMove.x > 0)
        {
            _scale.x = Mathf.Abs(_scale.x); // Ensure x scale is positive
            facingDir = 1;
        }
        // If the character is moving to the left, set local scale to flipped
        else if (_inputMove.x < 0)
        {
            _scale.x = -Mathf.Abs(_scale.x); // Flip x scale
            facingDir = -1;
        }

        transform.localScale = _scale;
    }

    private void Controller()
    {
        if (!CheckPlayerIsDead())
        {
            _inputMove.x = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                _rb.AddForce(new Vector2(_rb.velocity.x, _jumpForce));
                _playerAnim.SetTrigger("GoingUp");
            }

            _attackDelay -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.K) && _attackDelay < 0)
            {
                _playerAnim.SetTrigger("Attack");
                _attackDelay = 1f;
            }
        }
    }

    public bool isGrounded()
    {
       return Physics2D.OverlapCircle(_groundCheck.position, _radius, _groundLayer);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_attackPos.position, (facingDir == 1? Vector2.right : Vector2.left) * _distanceAtk);
    }*/

    #endregion
}
