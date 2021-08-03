using System;
using UnityEngine;

public class DifficultyWindow : MonoBehaviour {
    public DifficultyData easyData;
    public DifficultyData mediumData;
    public DifficultyData hardData;
    public DifficultyData impossibleData;
    public event EventHandler StartGame;
    private static DifficultyWindow _instance;
    private void Awake() {
        _instance = this;
    }
    private void Start() {
        gameObject.SetActive(true);
    }

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
    public static DifficultyWindow GetInstance() => _instance;
}
