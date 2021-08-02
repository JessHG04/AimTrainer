using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour {
    public GameObject circle;
    public Rigidbody2D rb;
    private Vector2 _direction = new Vector2(0, 0);
    private float _speed;
    private void Start() {
        _speed = GameManager.GetInstance().GetTargetSpeed();
        while(_direction.x == 0 && _direction.y == 0) {
            _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        }
        Destroy(gameObject, GameManager.GetInstance().GetDestroyTime());
    }

    private void Update() {
        if(SceneManager.GetActiveScene().name == "MovingTargetScene") {
            //Debug.Log(Mathf.Round(transform.position.x) + " " + Mathf.Round(transform.position.y));
            //Debug.Log(rb.velocity);
            if(Mathf.Round(transform.position.x) == -84f){ //Left
                _direction.x = 1f;
            }else if(Mathf.Round(transform.position.x) == 84f){ //Right
                _direction.x = -1f;
            }

            if(Mathf.Round(transform.position.y) == 39){ //Up
                _direction.y = -1f;
            }else if(Mathf.Round(transform.position.y) == -45){ //Down
                _direction.y = 1f;
            }
            rb.velocity = _direction * _speed;
        }
    }

    private void OnMouseDown() {
        GameManager.GetInstance().TargetHitted();
        float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        distance = Mathf.RoundToInt(distance);
        int maxScore = 100;
        int scoreMultiplier = 10;
        int score = maxScore - (scoreMultiplier * (int)distance);
        //Debug.Log(distance + " " + score);

        GameManager.GetInstance().UpdateScore(score);
        var go = Instantiate(circle, transform.position, Quaternion.identity);
        var text = go.GetComponentInChildren<TextMesh>();
        text.text = score.ToString();;
        Destroy(gameObject);
    }
}