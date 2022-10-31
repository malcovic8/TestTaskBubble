using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{

    [SerializeField] 
    private KeyCode pauseCode = default;

    [SerializeField] 
    private GameObject pauseMenu = default;

    [SerializeField] 
    private Button continueButton = default;
    
    [SerializeField]
    private Button exitButon = default;
    
    private bool isPause = default;

    private void Awake()
    {
        continueButton.onClick.AddListener(ChangeStatePause);
    }

    private void OnDestroy()
    {
        continueButton.onClick.RemoveListener(ChangeStatePause);
    }

    private void Update() => CallPause();

    private void CallPause()
    {
        if (Input.GetKeyDown(pauseCode))
        {
            ChangeStatePause();
        }
    }

    private void ChangeStatePause()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0 : 1;
        pauseMenu.SetActive(isPause);
    }
}
