using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialOptionsWindow : MonoBehaviour{
    private static InitialOptionsWindow _instance;
    public InputField targetsInput;
    public event EventHandler StartGame;
    public static InitialOptionsWindow getInstance(){
        return _instance;
    }
    private void Awake() {
        _instance = this;
    }
    private void Start() {
        gameObject.SetActive(true);
    }

    public void PlayButton(){
        if(targetsInput.text != ""){
            int targets = int.Parse(targetsInput.text);
            if(targets > 0){
                GameManager.getInstance().setInitialTargets(targets);
            }
        }
        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }

    public void ExitButton(){
        //Go back to the main menu
    }
}