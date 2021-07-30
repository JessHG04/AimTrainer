using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDWindow : MonoBehaviour {
    #region Private Variables
    private Text _lifes;
    private Text _score;
    private Text _targetsLeft;

    #endregion

    private void Start() {
        InitialOptionsWindow.GetInstance().StartGame += GameStarted;
        GameManager.GetInstance().FinishGame += GameFinished;
        gameObject.SetActive(false);
        _lifes = transform.Find("Lifes").GetComponent<Text>();
        _score = transform.Find("Score").GetComponent<Text>();
        _targetsLeft = transform.Find("Targets Left").GetComponent<Text>();
    }

    private void Update() {
        _lifes.text = "LIFES: " + GameManager.GetInstance().GetLifes().ToString();
        _score.text = "SCORE: " + GameManager.GetInstance().GetScore().ToString();
        _targetsLeft.text = "TARGETS LEFT: " + GameManager.GetInstance().GetTargetsLeft().ToString();
    }

    private void GameStarted(object sender, EventArgs e) {
        gameObject.SetActive(true);
    }

    private void GameFinished(object sender, EventArgs e) {
        gameObject.SetActive(false);
    }
}
