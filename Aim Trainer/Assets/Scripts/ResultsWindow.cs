using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsWindow : MonoBehaviour{
    #region Private Variables
    private Text _scoreText;
    private Text _targetsHitText;
    private Text _shotsFiredText;
    private Text _accuracyText;

    #endregion

    private void Start(){
        //Finish event
        _scoreText = transform.Find("Score").GetComponent<Text>();
        _targetsHitText = transform.Find("Targets Hit").GetComponent<Text>();
        _shotsFiredText = transform.Find("Shots Fired").GetComponent<Text>();
        _accuracyText = transform.Find("Accuracy").GetComponent<Text>();
        Hide();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    public void RetryButtonClicked() {
        //SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ShareButtonClicked() {
        //SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Debug.Log("Share button clicked");
    }
    
    public void MainMenuButtonClicked() {
        //SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
