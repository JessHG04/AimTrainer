using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyWindow : MonoBehaviour {

    public DifficultyDataSO difficultyData;
    public event EventHandler StartGame;
    private static DifficultyWindow _instance;
    private void Awake() {
        _instance = this;
    }
    private void Start() {
        gameObject.SetActive(true);
    }

    public void EasyButton() {
        GameManager.GetInstance().InitDifficultyData(difficultyData.dataList[0]);
        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }

    public void MediumButton() {
        GameManager.GetInstance().InitDifficultyData(difficultyData.dataList[1]);
        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }

    public void HardButton() {
        GameManager.GetInstance().InitDifficultyData(difficultyData.dataList[2]);
        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }

    public void ImpossibleButton() {
        GameManager.GetInstance().InitDifficultyData(difficultyData.dataList[3]);
        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }
    public void ExitButton() {
        SceneManager.LoadScene("Initial Scene", LoadSceneMode.Single);
    }
    public static DifficultyWindow GetInstance() => _instance;
}
