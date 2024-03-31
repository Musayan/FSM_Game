using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerHealth _healthPlayer;
    [SerializeField] private UImanager _uiManager;
    [SerializeField] private PlayerControl _player;

    [Header("Game Objects")]
    [SerializeField] private GameObject _playerObj;

    [Header("Key Function")]
    [SerializeField] private GameObject _key;
    [SerializeField] private Transform _KeySpawn;
    private bool keySpawned;

    [Header("Gate Function")]
    [SerializeField] private Animator _animtorGate;

    /*[Header("Door Function")]
    [SerializeField] private Animator _animatorDoor;*/

 
    private void Start()
    {
        Time.timeScale = 1.0f;  

    }
    private void Update()
    {
        if (_player.CheckPlayerIsDead())
            StartCoroutine(GameOverSet());

        if (!keySpawned && SpawnKey())
        {
            Instantiate(_key, _KeySpawn.position, Quaternion.identity);
            keySpawned = true;
        }

        if (OpenDoor())
        {
            _animtorGate.SetBool("isGateOpen", true);
        }
    }

    private bool SpawnKey()
    {
        GameObject[] stageBoss = GameObject.FindGameObjectsWithTag("StageBoss");
        int stageBossCount = stageBoss.Length;

        if (stageBossCount <= 0)
        {
            return true;
        }
        else
            return false;
    }

    private bool OpenDoor()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyCount = enemy.Length;

        if (enemyCount <= 0)
        {
            return true;
        }
        else
            return false;

    }
    private IEnumerator GameOverSet()
    {
        yield return new WaitForSeconds(1.5f);
        _uiManager.GameOver();
        Time.timeScale = 0f;
    }
}
