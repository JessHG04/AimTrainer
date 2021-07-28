using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialOptionsWindow : MonoBehaviour{
    public InputField targetsInput;
    public event EventHandler StartGame;
    private void Start() {
        gameObject.SetActive(true);
        targetsInput = gameObject.GetComponent<InputField>();
    }

    public void PlayButton(){
        int targets = int.Parse(targetsInput.text);
        if(targets > 0){
            GameManager.getInstance().setInitialTargets(targets);
        }else{
            GameManager.getInstance().setInitialTargets(15);
        }
        gameObject.SetActive(false);
    }

    public void ExitButton(){
        //Go back to the main menu
    }
}
