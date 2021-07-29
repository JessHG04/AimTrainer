using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    #region Public Variables
    public event EventHandler FinishGame;
    public GameObject target;
    public GameObject dot;
    public enum State{
        WaitingToStart,
        CountDown,
        Playing,
        ShowingResults
    }

    #endregion

    #region Private Variables
    private static GameManager _instance;
    private Vector2 _mousePosition;
    
    [SerializeField]
    private Texture2D _cursorTexture;

    [SerializeField]
    private Text _getReadyText;
    private static float _score = 0.0f;
    private static float _targetsHit = 0.0f;
    private float _shotsFired = 0.0f;
    private float _accuracy = 0.0f;
    private int _targetsSpawned = 0;
    private int _targetsAmount = 15;
    private int _lifes = 3;
    private float _spawnTime = 1.0f;
    private State _gameState;

    #endregion
    
    #region Unity Methods
    public static GameManager GetInstance(){
        return _instance;
    }
    private void Awake() {
        _instance = this;
    }
    private void Start() {
        InitialOptionsWindow.GetInstance().StartGame += GameStarted;
        var _cursorHotSpot = new Vector2(_cursorTexture.width / 2, _cursorTexture.height / 2);
        Cursor.SetCursor(_cursorTexture, _cursorHotSpot, CursorMode.Auto);
        _getReadyText.gameObject.SetActive(true);
        _gameState = State.WaitingToStart;
    }

    private void GameStarted(object sender, EventArgs e) {
        _gameState = State.CountDown;
        StartCoroutine(nameof(GetReady));
    }

    private void Update() {
        switch (_gameState) {
            case State.WaitingToStart:
                break;
            case State.CountDown:
                break;
            case State.Playing:
                if(Input.GetMouseButtonDown(0)){
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = Camera.main.nearClipPlane;
                    Instantiate(dot, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
                    _shotsFired++;
                }
                break;
            case State.ShowingResults:
                break;
        }
    }

    #endregion

    #region Utility Methods
    private IEnumerator GetReady(){
        for(int x = 3; x >=1; x--){
            _getReadyText.text = x + "\n" +  "Get Ready! ";
            yield return new WaitForSeconds(1f);
        }
        _getReadyText.text = "Go!";
        yield return new WaitForSeconds(1f);
        _gameState = State.Playing;
        StartCoroutine(nameof(SpawnTargets));
    }

    private IEnumerator SpawnTargets(){
        _getReadyText.gameObject.SetActive(false);
        _gameState = State.Playing;

        for(int x = 0; x < _targetsAmount; x++){
            var _targetRandomPosition = new Vector2(Random.Range(-48f, 48f), Random.Range(-25f, 20f));
            Instantiate(target, _targetRandomPosition, Quaternion.identity);
            _targetsSpawned++;
            yield return new WaitForSeconds(_spawnTime);
        }
        yield return new WaitForSeconds(InitialOptionsWindow.GetInstance().GetDestroyTime());
        _gameState = State.ShowingResults;
        if(FinishGame != null) FinishGame(this, EventArgs.Empty);
    }

    private bool Shot(){
        return true;
    }
    public void TargetHitted(){
        _targetsHit++;
    }

    public void UpdateScore(float scoreMultiplier){
        float maxScore = 10f;
        _score += scoreMultiplier * maxScore;
    }

    public void SetInitialTargets(int amount){
        _targetsAmount = amount;
    }

    public void SetInitialLifes(int lifes){
        _lifes = lifes;
    }

    public void SetInitialSpawnTime(float time){
        _spawnTime = time;
    }
    public void LoseLife(){
        _lifes--;
        if(_lifes <= 0){
            StopAllCoroutines();
            _gameState = State.ShowingResults;
            if(FinishGame != null) FinishGame(this, EventArgs.Empty);
        }
    }
    public int GetLifes() => _lifes;
    public State GetState() => _gameState;
    public float GetScore() => _score;
    public float GetTargetsHit() => _targetsHit;
    public float GetTargetsSpawned() => _targetsSpawned;
    public float GetShotsFired() => _shotsFired;
    public float GetAccuracy(){
        _accuracy = _targetsHit / _shotsFired * 100f;
        _accuracy =(float) Math.Round(_accuracy, 2);
        return _accuracy;
    }

    public int GetTargetsLeft(){
        return _targetsAmount - _targetsSpawned;
    }

    #endregion
}