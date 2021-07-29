using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDWindow : MonoBehaviour{
    private Text _targetsLeft;
    private Text _lifes;

    private void Start() {
        InitialOptionsWindow.getInstance().StartGame += GameStarted;
        GameManager.GetInstance().FinishGame += GameFinished;
        gameObject.SetActive(false);
        _targetsLeft = transform.Find("Targets Left").GetComponent<Text>();
        _lifes = transform.Find("Lifes").GetComponent<Text>();
    }

    private void Update() {
        _targetsLeft.text = "TARGETS LEFT: " + GameManager.GetInstance().GetTargetsLeft().ToString();
        _lifes.text = "LIFES: " + GameManager.GetInstance().GetLifes().ToString();
    }

    private void GameStarted(object sender, EventArgs e) {
        Debug.Log("Game started");
        gameObject.SetActive(true);
    }

    private void GameFinished(object sender, EventArgs e) {
        gameObject.SetActive(false);
    }
}
