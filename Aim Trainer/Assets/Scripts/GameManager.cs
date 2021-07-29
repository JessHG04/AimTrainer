using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour{
    #region Public Variables
    public event EventHandler FinishGame;
    public GameObject target;
    public GameObject dot;
    //public event EventHandler OnStartPlaying;

    #endregion

    #region Private Variables
    private static GameManager _instance;
    
    [SerializeField]
    private Texture2D _cursorTexture;
    private Vector2 _cursorHotSpot;
    private Vector2 _mousePosition;

    [SerializeField]
    private Text _getReadyText;
    private static float _score = 0.0f;
    private static float _targetsHit = 0.0f;
    private float _shotsFired = 0.0f;
    private float _accuracy = 0.0f;
    private int _targetsAmount = 15;
    private Vector2 _targetRandomPosition;
    private State _gameState;

    private enum State{
        WaitingToStart,
        CountDown,
        Playing,
        ShowingResults
    }

    #endregion
    
    #region Unity Methods
    public static GameManager getInstance(){
        return _instance;
    }

    private void Awake() {
        _instance = this;
    }
    private void Start() {
        InitialOptionsWindow.getInstance().StartGame += GameStarted;
        _cursorHotSpot = new Vector2(_cursorTexture.width / 2, _cursorTexture.height / 2);
        Cursor.SetCursor(_cursorTexture, _cursorHotSpot, CursorMode.Auto);
        _getReadyText.gameObject.SetActive(true);
        _gameState = State.WaitingToStart;
    }

    private void GameStarted(object sender, EventArgs e) {
        //Debug.Log("Targets: " + _targetsAmount);
        _gameState = State.CountDown;
        StartCoroutine(nameof(GetReady));
    }

    private void Update() {
        switch (_gameState) {
            case State.WaitingToStart:
                //Debug.Log("Waiting to start...");
                break;
            case State.CountDown:
                //Debug.Log("Counting down...");
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

        for(int x = _targetsAmount; x > 0; x--){
            _targetRandomPosition = new Vector2(Random.Range(-48f, 48f), Random.Range(-25f, 25f));
            Instantiate(target, _targetRandomPosition, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
        //Call finish game
        _gameState = State.ShowingResults;
        if(FinishGame != null) FinishGame(this, EventArgs.Empty);
    }
    public void targetHitted(){
        _targetsHit++;
    }

    public void updateScore(float scoreMultiplier){
        float maxScore = 10f;
        _score += scoreMultiplier * maxScore;
    }

    public void setInitialTargets(int amount){
        _targetsAmount = amount;
    }

    public float getScore() => _score;
    public float getTargetsHit() => _targetsHit;
    public float getTargetsAmount() => _targetsAmount;
    public float getShotsFired() => _shotsFired;
    public float getAccuracy(){
        _accuracy = _targetsHit / _shotsFired * 100f;
        _accuracy =(float) Math.Round(_accuracy, 2);
        return _accuracy;
    }

    #endregion
}