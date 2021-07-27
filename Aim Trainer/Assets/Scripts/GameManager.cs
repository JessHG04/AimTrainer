using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    #region Private Variables
    private static GameManager _instance;

    [SerializeField]
    private GameObject _target;
    
    [SerializeField]
    private Texture2D _cursorTexture;
    private Vector2 _cursorHotSpot;
    private Vector2 _mousePosition;

    [SerializeField]
    private Text _getReadyText;

    [SerializeField]
    private GameObject _resultsPanel;

    [SerializeField]
    private Text _scoreText, _targetsHitText, _shotsFiredText, _accuracyText;
    private static float _score, _targetsHit;
    private float _shotsFired;
    private float _accuracy;
    private int _targetsAmount;
    private Vector2 _targetRandomPosition;

    #endregion
    
    #region Unity Methods
    public static GameManager getInstance(){
        return _instance;
    }

    private void Awake() {
        _instance = this;
    }
    private void Start() {
        _cursorHotSpot = new Vector2(_cursorTexture.width / 2, _cursorTexture.height / 2);
        Cursor.SetCursor(_cursorTexture, _cursorHotSpot, CursorMode.Auto);

        _getReadyText.gameObject.SetActive(false);
        _targetsAmount = 10;
        _score = 0;
        _shotsFired = 0;
        _targetsHit = 0;
        _accuracy = 0;
        StartGetReadyCoroutine();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            _shotsFired++;
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

        StartCoroutine(nameof(SpawnTargets));
    }

    private IEnumerator SpawnTargets(){
        _getReadyText.gameObject.SetActive(false);
        _score = 0;
        _shotsFired = 0;
        _targetsHit = 0;
        _accuracy = 0;

        for(int x = _targetsAmount; x > 0; x--){
            _targetRandomPosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
            Instantiate(_target, _targetRandomPosition, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }

        _resultsPanel.gameObject.SetActive(true);
        _scoreText.text = "Score: " + _score;
        _targetsHitText.text = "Targets hit: " + _targetsHit + " / " + _targetsAmount;
        _shotsFiredText.text = "Shots fired: " + _shotsFired;
        _accuracy = _targetsHit / _shotsFired * 100f;
        _accuracyText.text = "Accuracy: " + _accuracy + "%";
    }

    public void StartGetReadyCoroutine(){
        _resultsPanel.SetActive(false);
        _getReadyText.gameObject.SetActive(true);
        StartCoroutine(nameof(GetReady));
    }

    public void targetHitted(){
        _targetsHit++;
    }

    public void updateScore(float scoreMultiplier){
        float maxScore = 10f;
        _score += scoreMultiplier * maxScore;
    }

    #endregion
}