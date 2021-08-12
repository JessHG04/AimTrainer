using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour {
    [SerializeField] private GameObject _circle;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _targetSprite;
    [SerializeField] private Sprite _birdSprite;
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
        if(_currentSceneName == "Motionless Target Scene"){
            _spriteRenderer.sprite = _targetSprite;
            _collider.radius = 0.61f;
        }else{
            _spriteRenderer.sprite = _birdSprite;
            _collider.radius = 0.47f;
            _collider.offset = new Vector2(-0.04f, 0.0f);
        }
        if(_currentSceneName == "Motionless Target Scene" || _currentSceneName == "Moving Target Scene") {
            Destroy(gameObject, GameManager.GetInstance().GetDestroyTime());
        }
    }

    private void Update() {
        if(_currentSceneName == "Moving Target Scene") {
            CheckTargetOutScreen();
            CheckSpriteLookAt();
            _rigidbody.velocity = _direction * _speed;
        }

        if(_currentSceneName == "Target Tracking Scene") {
            _timeToChangeDirection -= Time.deltaTime;
            if(_timeToChangeDirection <= 0) {
                _timeToChangeDirection = 1f;
                _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                while(_direction.x == 0 && _direction.y == 0) {
                    _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                }
            }
            CheckTargetOutScreen();
            CheckSpriteLookAt();
            _rigidbody.velocity = _direction * _speed;
        }
    }

    private void CheckTargetOutScreen() {
        if(Mathf.Round(transform.position.x) <= -82){ //Left
            _direction.x = 1f;
        }else if(Mathf.Round(transform.position.x) >= 82f){ //Right
            _direction.x = -1f;
        }

        if(Mathf.Round(transform.position.y) >= 38){ //Up
            _direction.y = -1f;
        }else if(Mathf.Round(transform.position.y) <= -27){ //Down
            _direction.y = 1f;
        }
    }

    private void CheckSpriteLookAt() {
        if(_direction.x == -1f && _direction.y == 1f){ //Left and up
            transform.rotation = Quaternion.Euler(0, -180, 45);
        }else if(_direction.x == -1f && _direction.y == 0f){ //Left
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }else if(_direction.x == -1f && _direction.y == -1f){ //Left and down
            transform.rotation = Quaternion.Euler(0, -180, -45);
        }else if(_direction.x == 0f && _direction.y == 1f){ //Up
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }else if(_direction.x == 0f && _direction.y == -1f){ //Down
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }else if(_direction.x == 1f && _direction.y == 1f){ //Right and up
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }else if(_direction.x == 1f && _direction.y == 0f){ //Right
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else if(_direction.x == 1f && _direction.y == -1f){ //Right and down  
            transform.rotation = Quaternion.Euler(0, 0, -45);
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
        
        if(_currentSceneName != "Target Tracking Scene") {
            var go = Instantiate(_circle, transform.position, Quaternion.identity);
            var text = go.GetComponentInChildren<TextMesh>();
            text.text = score.ToString();
        }
        
        if(_currentSceneName == "Motionless Target Scene" || _currentSceneName == "Moving Target Scene") {
            Destroy(gameObject);
        }
    }

    private void GameFinished(object sender, EventArgs e) {
        if(_currentSceneName == "Target Tracking Scene") Destroy(gameObject);
    }
}