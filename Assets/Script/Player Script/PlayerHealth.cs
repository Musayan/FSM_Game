using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Animator _playerAnim;
    [SerializeField] public float healthMax = 50f;
    [SerializeField] private UImanager _uiManager;

    public float _currentHealth;

    private void Start()
    {
        _currentHealth = healthMax;
        _uiManager.HealthMaxUpdate(_currentHealth);
    }
  
    public void DamagePlayer(float damage)
    {
        _playerAnim.SetTrigger("Hurt");
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp (_currentHealth, 0, healthMax);     
        _uiManager.HealthBarUpdate(_currentHealth);
    }

    private void UpdateHealth(float health)
    {
        _uiManager.HealthBarUpdate(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthUp"))
        {
            _currentHealth += 5f;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, healthMax);
            UpdateHealth(_currentHealth);
            Destroy(collision.gameObject);
        }
    }
}
