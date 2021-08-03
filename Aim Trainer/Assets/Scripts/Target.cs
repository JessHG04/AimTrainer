using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour {
    public GameObject circle;
    public Rigidbody2D rb;
    private Vector2 _direction = new Vector2(0, 0);
    private string _currentSceneName;
    private float _speed;
    private float _timeToChangeDirection;
    private void Start() {
        GameManager.GetInstance().FinishGame += GameFinished;
        _speed = GameManager.GetInstance().GetTargetSpeed();
        _timeToChangeDirection = 0.5f;
        while(_direction.x == 0 && _direction.y == 0) {
            _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        }
        _currentSceneName = SceneManager.GetActiveScene().name;
        if(_currentSceneName == "MotionlessTargetScene" || _currentSceneName == "MovingTargetScene") {
            Destroy(gameObject, GameManager.GetInstance().GetDestroyTime());
        }
    }

    private void Update() {
        if(_currentSceneName == "MovingTargetScene") {
            CheckTargetOutScreen();
            rb.velocity = _direction * _speed;
        }

        if(_currentSceneName == "TargetTrackingScene") {
            _timeToChangeDirection -= Time.deltaTime;
            if(_timeToChangeDirection <= 0) {
                _timeToChangeDirection = 1f;
                _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                while(_direction.x == 0 && _direction.y == 0) {
                    _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                }
            }
            CheckTargetOutScreen();
            rb.velocity = _direction * _speed;
        }
    }

    private void CheckTargetOutScreen() {
        if(Mathf.Round(transform.position.x) <= -84f){ //Left
            _direction.x = 1f;
        }else if(Mathf.Round(transform.position.x) >= 84f){ //Right
            _direction.x = -1f;
        }

        if(Mathf.Round(transform.position.y) >= 39){ //Up
            _direction.y = -1f;
        }else if(Mathf.Round(transform.position.y) <= -45){ //Down
            _direction.y = 1f;
        }
    }

    private void OnMouseDown() {
        GameManager.GetInstance().TargetHitted();
        float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        distance = Mathf.RoundToInt(distance);
        int maxScore = 100;
        int scoreMultiplier = 10;
        int score = maxScore - (scoreMultiplier * (int)distance);

        GameManager.GetInstance().UpdateScore(score);
        var go = Instantiate(circle, transform.position, Quaternion.identity);
        var text = go.GetComponentInChildren<TextMesh>();
        text.text = score.ToString();
        if(_currentSceneName == "MotionlessTargetScene" || _currentSceneName == "MovingTargetScene") {
            Destroy(gameObject);
        }
    }

    private void GameFinished(object sender, EventArgs e) {
        Destroy(gameObject);
    }
}