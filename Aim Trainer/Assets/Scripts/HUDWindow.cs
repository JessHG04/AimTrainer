using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDWindow : MonoBehaviour {
    private Text _lifes;
    private Text _score;
    private Text _targetsLeft;
    private Text _timeLeft;
    private string _currentSceneName;

    private void Start() {
        DifficultyWindow.GetInstance().StartGame += GameStarted;
        GameManager.GetInstance().FinishGame += GameFinished;
        gameObject.SetActive(false);
        _lifes = transform.Find("Lifes").GetComponent<Text>();
        _score = transform.Find("Score").GetComponent<Text>();
        Text targetsLeftText = transform.Find("Targets Left Text").GetComponent<Text>();
        _targetsLeft = transform.Find("Targets Left").GetComponent<Text>();
        Text timeLeftText = transform.Find("Time Left Text").GetComponent<Text>();
        _timeLeft = transform.Find("Time Left").GetComponent<Text>();
        _currentSceneName = SceneManager.GetActiveScene().name;
        if(_currentSceneName == "Target Tracking Scene"){
            targetsLeftText.text = "";
            _targetsLeft.text = "";
        }else{
            timeLeftText.text = "";
            _timeLeft.text = "";
        }
    }

    private void Update() {
        _lifes.text = GameManager.GetInstance().GetLifes().ToString();
        _score.text = GameManager.GetInstance().GetScore().ToString();
        if(_currentSceneName == "Target Tracking Scene"){
            _timeLeft.text = GameManager.GetInstance().GetTimeLeft().ToString();
        }else{
            _targetsLeft.text = GameManager.GetInstance().GetTargetsLeft().ToString();
        }
    }

    private void GameStarted(object sender, EventArgs e) {
        gameObject.SetActive(true);
    }

    private void GameFinished(object sender, EventArgs e) {
        gameObject.SetActive(false);
    }
}
