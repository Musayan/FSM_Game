using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header ("Enemy Function")]
    [SerializeField] private GameObject _enemy;
    private Rigidbody2D _rb;
    private Animator _enemyAnim;
    public float _enemyHealth;

    [Header("Enemy Slider")]
    [SerializeField] private Slider _sliderHealth;
    [SerializeField] private GameObject _sliderObj;

    private void Start()
    {
        _enemyAnim = GetComponent<Animator>();  
        _rb = GetComponent<Rigidbody2D>();
        SetHeatlhBar();
        _sliderObj.SetActive(false);
    }

    private void Update()
    {
        if (_enemyHealth < 0)
        {
            StartCoroutine(Dead());
            _sliderObj.SetActive(false);
        }

        _sliderHealth.value = _enemyHealth; 
    }

    private IEnumerator Dead()
    {
        _enemy.GetComponent<CapsuleCollider2D>().enabled = false;
        _rb.gravityScale = 0f;
        _rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        Destroy(_enemy);
    }
    public void damageEnemy(float damage)
    {
        _enemyHealth -= damage;
        _enemyAnim.SetTrigger("Hurt");
        _sliderObj.SetActive(true);
    }

    public void SetHeatlhBar()
    {
       _sliderHealth.maxValue = _enemyHealth;   
       _sliderHealth.value = _enemyHealth;
    }
}
