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
        _targetsLeft = transform.Find("Targets Left").GetComponent<Text>();
        _timeLeft = transform.Find("Time Left").GetComponent<Text>();
        _currentSceneName = SceneManager.GetActiveScene().name;
        if(_currentSceneName == "TargetTrackingScene"){
            _targetsLeft.text = "";
        }else{
            _timeLeft.text = "";
        }
    }

    private void Update() {
        _lifes.text = "LIFES: " + GameManager.GetInstance().GetLifes().ToString();
        _score.text = "SCORE: " + GameManager.GetInstance().GetScore().ToString();
        if(_currentSceneName == "TargetTrackingScene"){
            _timeLeft.text = "TIME LEFT: " + GameManager.GetInstance().GetTimeLeft().ToString();
        }else{
            _targetsLeft.text = "TARGETS LEFT: " + GameManager.GetInstance().GetTargetsLeft().ToString();
        }
    }

    private void GameStarted(object sender, EventArgs e) {
        gameObject.SetActive(true);
    }

    private void GameFinished(object sender, EventArgs e) {
        gameObject.SetActive(false);
    }
}
