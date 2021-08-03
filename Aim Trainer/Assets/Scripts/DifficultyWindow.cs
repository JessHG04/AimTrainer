using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyWindow : MonoBehaviour {
    public event EventHandler StartGame;
    public DifficultyData easyData;
    public DifficultyData mediumData;
    public DifficultyData hardData;
    public DifficultyData impossibleData;

    #region Private Variables
    private static DifficultyWindow _instance;

    #endregion
    private void Awake() {
        _instance = this;
    }
    private void Start() {
        gameObject.SetActive(true);
    }

    public static DifficultyWindow GetInstance() => _instance;

    public void EasyButton() {
        GameManager.GetInstance().InitDifficultyData(easyData);
        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }

    public void MediumButton() {
        GameManager.GetInstance().InitDifficultyData(mediumData);
        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }

    public void HardButton() {
        GameManager.GetInstance().InitDifficultyData(hardData);
        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }

    public void ImpossibleButton() {
        GameManager.GetInstance().InitDifficultyData(impossibleData);
        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }
    public void ExitButton() {
        //Go back to the main menu
        Application.Quit();
    }
}
