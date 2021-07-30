using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour {
    public GameObject circle;
    public Rigidbody2D rb;
    private Vector2 _direction;
    private float _speed;
    private void Start() {
        _speed = GameManager.GetInstance().GetTargetSpeed();
        _direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        Destroy(gameObject, InitialOptionsWindow.GetInstance().GetDestroyTime());
    }

    private void Update() {
        if(SceneManager.GetActiveScene().name == "MovingTargetsScene") {
            rb.velocity = _direction * _speed;
            Debug.Log(rb.velocity);
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
        Instantiate(circle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}