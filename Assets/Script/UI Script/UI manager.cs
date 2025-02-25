using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    //Player Health UI
    [Header("Player Health")]
    [SerializeField] private GameObject _healthSlider;
    [SerializeField] private Slider _sliderHealth;


    [Header("Boss Attributes")]
    [SerializeField] private Slider _bossHealth;
    [SerializeField] private Slider _bossPowerUp;
    [SerializeField] private GameObject _bossAttributesObj;

    //GameOver
    [Header ("Overlay Panel")]
    [SerializeField] private GameObject _gameOverPanel;


    private void Start()
    {
        StartPowerUp();
    }

    #region Boss_powerUpBar

    public void StartPowerUp()
    {
        if (_bossPowerUp != null)
        {
            _bossPowerUp.maxValue = 30f;
            _bossPowerUp.value = 0f;
        }
    }

    public void UpdatedPowerUpBar(float value)
    {
        _bossPowerUp.value = value;
    }

    #endregion

    #region Boss_healthBar
    public void UpdatedBossHealth(float healthBoss)
    {
        _bossHealth.maxValue = healthBoss;
        _bossHealth.value = healthBoss; 
    }

    public void UpdatedDamagehealth(float damageBoss)
    {
        _bossHealth.value = damageBoss;
    }

    #endregion

    #region HealthBar

    public void HealthMaxUpdate(float healthMax)
    {
        _sliderHealth.maxValue = healthMax;
        _sliderHealth.value = healthMax;
    }

    public void HealthBarUpdate(float Health)
    {
        _sliderHealth.value = Health;
    }

    #endregion

    #region GameOver

    public void GameOver()
    {
        _healthSlider.SetActive(false);
        _gameOverPanel.SetActive(true);

        if (_bossAttributesObj != null)
            _bossAttributesObj.SetActive(false);    
        
    }

    #endregion
}
