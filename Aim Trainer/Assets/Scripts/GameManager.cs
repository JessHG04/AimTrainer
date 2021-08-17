using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    
    [SerializeField]
    private Texture2D _cursorTexture;

    [SerializeField]
    private Text _getReadyText;
    private int _score = 0;
    private int _targetsHit = 0;
    private int _shotsFired = 0;
    private float _accuracy = 0.0f;
    private int _targetsSpawned = 0;
    private int _targetsAmount;
    private float _targetSpeed;
    private int _lifes;
    private float _spawnTime;
    private float _destroyTime;
    private State _gameState;
    private float _timeToFinish;
    private string _currentSceneName;

    #endregion
    
    #region Unity Methods
    private void Awake() {
        _instance = this;
    }
    private void Start() {
        DifficultyWindow.GetInstance().StartGame += GameStarted;
        var _cursorHotSpot = new Vector2(_cursorTexture.width / 2, _cursorTexture.height / 2);
        Cursor.SetCursor(_cursorTexture, _cursorHotSpot, CursorMode.Auto);
        _getReadyText.gameObject.SetActive(true);
        _currentSceneName = SceneManager.GetActiveScene().name;
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
                if(Input.GetMouseButtonDown(0)) {
                    Vector3 mousePos = Input.mousePosition;
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                    mousePos.z = 0.0f;
                    Instantiate(dot, mousePos, Quaternion.identity);
                    _shotsFired++;
                }
                
                if(GetTargetsLeft() == 0 && GetTargetsInScreen() == 0) {
                    _gameState = State.ShowingResults;
                    if(FinishGame != null) FinishGame(this, EventArgs.Empty);
                }

                if(_currentSceneName == "Target Tracking Scene") {
                    _timeToFinish -= Time.deltaTime;
                    if(_timeToFinish <= 0) {
                        _gameState = State.ShowingResults;
                        if(FinishGame != null) FinishGame(this, EventArgs.Empty);
                    }
                }

                break;
            case State.ShowingResults:
                break;
        }
    }

    #endregion

    #region Utility Methods
    public void InitDifficultyData(DifficultyData data) {
        _lifes = data.lifes;
        _targetsAmount = data.targetsAmount;
        _spawnTime = data.timeToSpawnTarget;
        _destroyTime = data.timeToDestroyTarget;
        _targetSpeed = data.targetSpeed;
        _timeToFinish = data.timeToFinishGame;
    }

    private IEnumerator GetReady() {
        for(int x = 3; x >= 1; x--) {
            _getReadyText.text = x + "\n" +  "Get Ready! ";
            yield return new WaitForSeconds(1f);
        }
        _getReadyText.text = "Go!";
        yield return new WaitForSeconds(1f);
        _gameState = State.Playing;
        StartCoroutine(nameof(SpawnTargets));
    }

    private IEnumerator SpawnTargets() {
        _getReadyText.gameObject.SetActive(false);
        _gameState = State.Playing;

        for(int x = 0; x < _targetsAmount; x++) {
            if(_currentSceneName == "Target Tracking Scene") {
                Instantiate(target, new Vector3(0, 0, 0), Quaternion.identity);
            } else {
                var _targetRandomPosition = new Vector2(Random.Range(-82f, 82f), Random.Range(-27f, 38f));
                Instantiate(target, _targetRandomPosition, Quaternion.identity);
            }
            _targetsSpawned++;
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    public void LoseLife() {
        _lifes--;
        if(_lifes <= 0) {
            StopAllCoroutines();
            _gameState = State.ShowingResults;
            if(FinishGame != null) FinishGame(this, EventArgs.Empty);
        }
    }

    public void TargetHitted() => _targetsHit++;
    public void UpdateScore(int scoreAdded) => _score += scoreAdded;

    #endregion

    #region Getters & Setters
    public static GameManager GetInstance() => _instance;
    public int GetLifes() => _lifes;
    public State GetState() => _gameState;
    public int GetScore() => _score;
    public int GetTargetsHit() => _targetsHit;
    public int GetTargetsSpawned() => _targetsSpawned;
    public int GetShotsFired() => _shotsFired;
    public int GetTargetsLeft() => _targetsAmount - _targetsSpawned;
    public float GetDestroyTime() => _destroyTime;
    public float GetTargetSpeed() => _targetSpeed;
    public int GetTimeLeft() => Mathf.RoundToInt(_timeToFinish);
    public float GetAccuracy() {
        _accuracy = ((float) _targetsHit / _shotsFired) * 100f;
        _accuracy =(float) Math.Round(_accuracy, 2);
        return _accuracy;
    }

    public int GetTargetsInScreen() {
        var screenTargets = GameObject.FindObjectsOfType<Target>();
        return screenTargets.Length;
    }

    #endregion
}