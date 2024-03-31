using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    //Player Health UI
    [SerializeField] private GameObject _healthSlider;
    [SerializeField] private Slider _sliderHealth;

    //GameOver
    [SerializeField] private GameObject _gameOverPanel;

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
    }

    #endregion
}
