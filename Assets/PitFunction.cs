using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFunction : MonoBehaviour
{

    [SerializeField] private Transform _spawnPos;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GameObject.FindAnyObjectByType<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = _spawnPos.position;
            _playerHealth.DamagePlayer(10f);
        }
    }
}
