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
    private Vector2 _direction;
    private string _currentSceneName;
    private float _speed;
    private float _timeToChangeDirection = 1.5f;
    private const float LeftLimit = -84f;
    private const float RightLimit = 84f;
    private const float TopLimit = 38f;
    private const float BottomLimit = -27f;
    private void Start() {
        GameManager.GetInstance().FinishGame += GameFinished;
        _speed = GameManager.GetInstance().GetTargetSpeed();
        _currentSceneName = SceneManager.GetActiveScene().name;
        Random.InitState((int)System.DateTime.Now.Ticks);
        NewDirection();
        
        if(_currentSceneName == "Motionless Target Scene"){
            _spriteRenderer.sprite = _targetSprite;
            _collider.radius = 0.61f;
            _collider.offset = new Vector2(0, 0);
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
        Random.InitState(System.DateTime.Now.Millisecond);
        if(_currentSceneName == "Moving Target Scene") {
            CheckTargetOutScreen();
            CheckSpriteLookAt();
            _rigidbody.velocity = _direction * _speed;
        }

        if(_currentSceneName == "Target Tracking Scene") {
            _timeToChangeDirection -= Time.deltaTime;
            if(!CheckTargetOutScreen() && _timeToChangeDirection <= 0.0f) {
                //Debug.Log("Change Direction");
                NewDirection();
            }
            CheckSpriteLookAt();
            _rigidbody.velocity = _direction * _speed;
        }
    }

    private void NewDirection(){
        int currentX = (int) _direction.x;
        int currentY = (int) _direction.y;
        int newX = Random.Range(-1, 2);
        int newY = Random.Range(-1, 2);

        while((newX == 0 && newY == 0) || (newX == currentX && newY == currentY)) {
            newX = Random.Range(-1, 2);
            newY = Random.Range(-1, 2);
        }

        _direction = new Vector2(newX, newY);
        _timeToChangeDirection = 1.5f;
        //Debug.Log("OldX: " + currentX + " OldY: " + currentY);
        //Debug.Log("NewX: " + newX + " NewY: " + newY);
        //Debug.Log(" ");
    }

    private bool CheckTargetOutScreen() {
        bool changed = false;        
        
        if((Mathf.Round(transform.position.x) <= LeftLimit) && (Mathf.Round(transform.position.y) >= TopLimit)) { //Left and up
            transform.position = new Vector2(LeftLimit + 1f, TopLimit - 1f);
            _direction = new Vector2(1f, -1f);
            changed = true;
            //Debug.Log("Left and up");
        }else if((Mathf.Round(transform.position.x) <= LeftLimit) && (Mathf.Round(transform.position.y) <= BottomLimit) && !changed) { //Left and down
            transform.position = new Vector2(LeftLimit + 1f, BottomLimit + 1f);
            _direction = new Vector2(1f, 1f);
            changed = true;
            //Debug.Log("Left and down");
        }else if((Mathf.Round(transform.position.x) >= RightLimit) && (Mathf.Round(transform.position.y) >= TopLimit) && !changed) { //Right and up
            transform.position = new Vector2(RightLimit - 1f, TopLimit - 1f);
            _direction = new Vector2(-1f, -1f);
            changed = true;
            //Debug.Log("Right and up");
        }else if((Mathf.Round(transform.position.x) >= RightLimit) && (Mathf.Round(transform.position.y) <= BottomLimit) && !changed) { //Right and down  
            transform.position = new Vector2(RightLimit - 1f, BottomLimit + 1f);
            _direction = new Vector2(-1f, 1f);
            changed = true;
            //Debug.Log("Right and down");
        }else if((Mathf.Round(transform.position.x) <= LeftLimit) && !changed) { //Left
            transform.position = new Vector2(LeftLimit + 1f, transform.position.y);
            _direction = new Vector2(1f, _direction.y);
            changed = true;
            //Debug.Log("Left");
        }else if((Mathf.Round(transform.position.x) >= RightLimit) && !changed) { //Right
            transform.position = new Vector2(RightLimit - 1f, transform.position.y);
            _direction = new Vector2(-1f, _direction.y);
            changed = true;
            //Debug.Log("Right");
        }else if((Mathf.Round(transform.position.y) >= TopLimit) && !changed ){ //Up
            transform.position = new Vector2(transform.position.x, TopLimit - 1f);
            _direction = new Vector2(_direction.x, -1f);
            changed = true;
            //Debug.Log("Up");
        }else if((Mathf.Round(transform.position.y) <= BottomLimit) && !changed) { //Down
            transform.position = new Vector2(transform.position.x, BottomLimit + 1f);
            _direction = new Vector2(_direction.x, 1f);
            changed = true;
            //Debug.Log("Down");
        }

        //if(changed) _timeToChangeDirection = 1.5f;
        
        return changed;
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
            var go = Instantiate(_circle, transform.position, transform.rotation);
            var text = go.GetComponentInChildren<TextMesh>();
            text.text = score.ToString();
        }
        
        if(_currentSceneName == "Motionless Target Scene" || _currentSceneName == "Moving Target Scene") {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, _direction * 10f);
    }

    private void GameFinished(object sender, EventArgs e) {
        if(_currentSceneName == "Target Tracking Scene") Destroy(gameObject);
    }
}