using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const string FORMAT_TIMER = "{0}:{1}";
    private const float TIMER_MIN = 2f;
    
    [SerializeField] private GameObject menuFinishGame = default;

    [SerializeField] private GameObject menuGameOver = default;

    [SerializeField] private Text timerText = default;

    [SerializeField] private PauseController pauseController = default;
    
    private float timeRemaining = default;
    float minutes = default; 
    float seconds = default;


    private Coroutine timerCoroutine = default;
    
    public void FinishGame()
    {
        CallMenu(menuFinishGame);
    } 
    
    private void Start()
    {
        if (PlayerPrefs.GetString(PrefsHelper.PREFS_GAME_MODE) == PrefsHelper.PREFS_TIME_MODE)
        {
            timerText.enabled = true;
            if (timerCoroutine == null)
            {
                timerCoroutine = StartCoroutine(StartTimer());
            }
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
            CheckTimer(minutes);
            yield return null;
        }
    }

    private void CheckTimer(float minutes)
    {
        if (TIMER_MIN == minutes)
        {
            CallMenu(menuGameOver);
        }
    }
    
    private void CallMenu(GameObject callMenu)
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        pauseController.gameObject.SetActive(false);
        callMenu.SetActive(true);
    }

}
