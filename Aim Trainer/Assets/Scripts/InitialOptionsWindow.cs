using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialOptionsWindow : MonoBehaviour {
    public event EventHandler StartGame;
    private static InitialOptionsWindow _instance;
    private InputField _lifesInput;
    private InputField _targetsInput;
    private InputField _spawnTimeInput;
    private InputField _destroyTimeInput;
    private float _destroyTime = 1.0f;
    
    public static InitialOptionsWindow GetInstance(){
        return _instance;
    }
    private void Awake() {
        _instance = this;
    }
    private void Start() {
        gameObject.SetActive(true);
        _lifesInput = transform.Find("Lifes Input").GetComponent<InputField>();
        _targetsInput = transform.Find("Targets Input").GetComponent<InputField>();
        _spawnTimeInput = transform.Find("Spawn Time Input").GetComponent<InputField>();
        _destroyTimeInput = transform.Find("Destroy Time Input").GetComponent<InputField>();
    }

    public void PlayButton(){
        if(_lifesInput.text != ""){
            int lifes = int.Parse(_lifesInput.text);
            if(lifes > 0){
                GameManager.GetInstance().SetInitialLifes(lifes);
            }
        }

        if(_targetsInput.text != ""){
            int targets = int.Parse(_targetsInput.text);
            if(targets > 0){
                GameManager.GetInstance().SetInitialTargets(targets);
            }
        }

        if(_spawnTimeInput.text != ""){
            float spawnTime = float.Parse(_spawnTimeInput.text);
            if(spawnTime > 0.0f){
                GameManager.GetInstance().SetInitialSpawnTime(spawnTime);
            }
        }

        if(_destroyTimeInput.text != ""){
            _destroyTime = float.Parse(_destroyTimeInput.text);
            if(_destroyTime <= 0.0f){
                _destroyTime = 1.0f;
            }
        }

        gameObject.SetActive(false);
        if(StartGame != null) StartGame(this, EventArgs.Empty);
    }

    public float GetDestroyTime(){
        return _destroyTime;
    }

    public void ExitButton(){
        //Go back to the main menu
    }
}
