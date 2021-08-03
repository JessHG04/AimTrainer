using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsWindow : MonoBehaviour {
    private Text _scoreText;
    private Text _targetsHitText;
    private Text _shotsFiredText;
    private Text _accuracyText;
    private string _currentSceneName;

    private void Start() {
        GameManager.GetInstance().FinishGame += GameFinished;
        _scoreText = transform.Find("Score").GetComponent<Text>();
        _targetsHitText = transform.Find("Targets Hit").GetComponent<Text>();
        _shotsFiredText = transform.Find("Shots Fired").GetComponent<Text>();
        _accuracyText = transform.Find("Accuracy").GetComponent<Text>();
        _currentSceneName = SceneManager.GetActiveScene().name;
        Hide();
    }

    private void GameFinished(object sender, EventArgs e) {
        _scoreText.text = "Score: " + GameManager.GetInstance().GetScore().ToString();
        _targetsHitText.text = "Targets Hit: " + GameManager.GetInstance().GetTargetsHit().ToString() + " / " + GameManager.GetInstance().GetTargetsSpawned().ToString();
        _shotsFiredText.text = "Shots Fired: " + GameManager.GetInstance().GetShotsFired().ToString();
        _accuracyText.text = "Accuracy: " + GameManager.GetInstance().GetAccuracy().ToString() + "%";
        Show();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    public void RetryButtonClicked() {
        SceneManager.LoadScene(_currentSceneName, LoadSceneMode.Single);
    }

    public void ShareButtonClicked() {
        Debug.Log("Share button clicked");
    }
    
    public void ExitButtonClicked() {
        SceneManager.LoadScene("Initial Scene", LoadSceneMode.Single);
    }
}
