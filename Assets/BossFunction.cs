using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BossFunction : MonoBehaviour
{
    [Header("Draw Gizmo")]
    [SerializeField] private bool _gizmosAttackRange;

    //Boss Attributes
    [Header("Boss Attributes")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] public float _setDelay;
    [HideInInspector] public float _attackDelay;
    private Rigidbody2D _rbBoss;
    private int _facingLeft = 1;

    //Detection Function
    [Header("Detection Variables")]
    [SerializeField] private LayerMask _groundCheck;
    [SerializeField] private LayerMask _playerDetection;
    [SerializeField] private Transform _bossRange;
    [SerializeField] private float _radius;
    [HideInInspector] public bool _canDetect;
    

    [Header("Boss Ultimate")]
    [SerializeField] private float _UltimateSpell;
    [SerializeField] GameObject _skeleton, _bigSkeleton, _Hand;
    [HideInInspector] public bool _powerUpStart;

    //Finding Player
    [Header ("For enemy Rotation")]
    [SerializeField] private Transform _enemyPos;
    private Transform _playerPos;

    //Boss Health
    [Header("Boss Health")]
    [SerializeField] private float _healthAmount;
    private UImanager _UIManager;
    
    [Header("Animator")]
    [SerializeField] private Animator _bossAnim;


    private PlayerHealth _playerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        _UIManager = GameObject.FindAnyObjectByType<UImanager>();
        _playerHealth = GameObject.FindAnyObjectByType<PlayerHealth>(); 
        _UIManager.UpdatedBossHealth(_healthAmount);
        _rbBoss = GetComponent<Rigidbody2D>();
        _powerUpStart = true;
        _facingLeft = 1;
        OnEnableBoss();
    }

    // Update is called once per frame
    void Update()
    {
        _attackDelay -= Time.deltaTime;
        
        if (_powerUpStart)
        {
            _UltimateSpell += Time.deltaTime;
            _UIManager.UpdatedPowerUpBar(_UltimateSpell);

            if (CanUltimateBoss())
            {
                _bossAnim.SetTrigger("Cast");
                OnDisableBoss();
                _UltimateSpell = 0;
                
            }
        }
        
        FindPlayer();
    }

    public void OnEnableBoss()
    {
        _canDetect = true;
        _rbBoss.bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public void OnDisableBoss()
    {
        _canDetect = false;
        _rbBoss.bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    //Making the Boss Face to the player position;
    #region FaceToPlayer

    private void FindPlayer()
    {
        if (_playerPos.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _facingLeft = 0;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _facingLeft = 1;
        }
            
    }
    #endregion

    //BossWalking
    #region Boss_walking

    public void BossWalk()
    {
       Vector2 moveDirection = _facingLeft == 1? Vector2.left : Vector2.right;

        _rbBoss.velocity = new Vector2(moveDirection.x * _moveSpeed * Time.deltaTime, _rbBoss.velocity.y);
    }

    #endregion

    //Player Detection
    #region Player_Detection

    public bool PlayerInRange()
    {
        if (_canDetect)
        {
            RaycastHit2D playerNear = Physics2D.CircleCast(_bossRange.position, _radius, Vector2.zero, _playerDetection);

            if (playerNear.collider.CompareTag("Player"))
            {
                return true;
            }
            else
            { return false; }
        }
        else
            return false;
    }

    #endregion

    //Boss Attack
    #region Boss_attack

    public void BossAttackInrange()
    {
        RaycastHit2D playerNear = Physics2D.CircleCast(_bossRange.position, _radius, Vector2.zero, _playerDetection);

        if (playerNear.collider.CompareTag("Player"))
        {
            _playerHealth.DamagePlayer(10f);
        }

        _attackDelay = _setDelay;
    }
    
    //Checking if can attack
    public bool CanAttack()
    {
        if (_attackDelay <= 0)
            return true;
        else 
            return false;
    }

    #endregion

    //Boss Ultimate
    #region Boss_ultimate
    
    private bool CanUltimateBoss()
    {
        if (_UltimateSpell > 30f)
            return true;
        else
            return false;
    }

    public void BossUltimate()
    {
        int chooseUlt = Random.Range(0, 3);
        
        switch (chooseUlt)
        {
            case 0:
                StartCoroutine(SpawnSkeleton(5, 5f));
                break;
            case 1:
                StartCoroutine(SpawnBigSkeleton(3, 5f));
                break;
            case 2:
                StartCoroutine(SpawnHand(5, 2f));
                break;
        }  
    }

    //Skeleton spawners
    private IEnumerator SpawnSkeleton(int numberOfSpawn, float delay) 
    {
        int spawnedCount = 0;
        bool canSpawn = true;
        int SpawnCount = 0;

        while (spawnedCount < numberOfSpawn)
        {
            GameObject[] SpawnedEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            SpawnCount = SpawnedEnemy.Length;
            
            if (canSpawn && SpawnCount <= 0)
            {
                Vector3 spawnPos = new Vector3(_playerPos.position.x - 1, 3f, 0f);
                Instantiate(_skeleton, spawnPos, Quaternion.identity);
                spawnedCount++;
                canSpawn = false;
            }
            yield return new WaitForSeconds(delay);
            
            if (SpawnCount <= 0)
            canSpawn = true;
        }

        // Signal the end of spawning
        yield return new WaitForSeconds (delay);        
        _bossAnim.SetTrigger("Finish");
        yield return null;
        
        
    }

    //Big Skeleton Spawner
    private IEnumerator SpawnBigSkeleton(int numberOfSpawn, float delay)
    {
        int spawnedCount = 0;
        bool canSpawn = true;
        int SpawnCount = 0;

        while (spawnedCount < numberOfSpawn)
        {
            GameObject[] SpawnedBigSkel = GameObject.FindGameObjectsWithTag("StageBoss");
            SpawnCount = SpawnedBigSkel.Length;   

            if (canSpawn && SpawnCount <= 0)
            {
                Vector3 spawnPos = new Vector3(_playerPos.position.x - 1, 3f, 0f);
                Instantiate(_bigSkeleton, spawnPos, Quaternion.identity);
                spawnedCount++;
                canSpawn = false;
            }   
            yield return new WaitForSeconds(delay);

            if (SpawnCount <= 0)
                canSpawn = true;
        }

        yield return new WaitForSeconds(delay);
        _bossAnim.SetTrigger("Finish");
        yield return null;
 
    }

    private IEnumerator SpawnHand(int numberOfSpawn, float delay)
    {
        int spawnedCount = 0;
        bool canSpawn = true;

        while (spawnedCount < numberOfSpawn)
        {
            if (canSpawn)
            {
                Vector3 spawnPos = new Vector3(_playerPos.position.x, -3.42f, 0f);
                Instantiate(_Hand, spawnPos, Quaternion.identity);
                spawnedCount++;
                canSpawn = false;
            }
            yield return new WaitForSeconds(delay);
            canSpawn = true;
        }

        _bossAnim.SetTrigger("Finish");
        yield return null;
    }
    #endregion

    //Boss Health Function
    #region Boss_Health
    
    public void DamageBoss( float damage )
    {
        _healthAmount -= damage;
        _UIManager.UpdatedDamagehealth(_healthAmount);
        _bossAnim.SetTrigger("Hurt");
    }

    public bool bossDead()
    {
        if (_healthAmount <= 0)
            return true;
        else 
            return false;
    }

    #endregion

    //Gizmo Draw
    private void OnDrawGizmos()
    {
        if (_gizmosAttackRange)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(_bossRange.position, _radius);
        }
    }
}
