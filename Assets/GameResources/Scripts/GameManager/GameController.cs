using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const string FORMAT_TIMER = "{0}:{1}";
    private const int TIMER_MIN = 2;
    
    [SerializeField] private GameObject menuFinishGame = default;

    [SerializeField] private GameObject menuGameOver = default;

    [SerializeField] private Text timerText = default;

    private float timeRemaining = default;
    float minutes = default; 
    float seconds = default;
    
    public void FinishGame() =>  menuGameOver.SetActive(true);
    private void Start()
    {
        if (PlayerPrefs.GetString(PrefsHelper.PREFS_GAME_MODE) == PrefsHelper.PREFS_TIME_MODE)
        {
            timerText.enabled = true;
            StartCoroutine(StartTimer());
        }
    }

    private IEnumerator StartTimer()
    {
        while (isActiveAndEnabled && minutes < TIMER_MIN)
        {
            timeRemaining += Time.deltaTime;
            minutes = Mathf.FloorToInt(timeRemaining / 60);
            seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format(FORMAT_TIMER, minutes, seconds);
            yield return null;
        }
    }
        
}
