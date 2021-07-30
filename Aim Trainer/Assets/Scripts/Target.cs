using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public GameObject circle;
    void Start() {
        Destroy(gameObject, InitialOptionsWindow.GetInstance().GetDestroyTime());
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