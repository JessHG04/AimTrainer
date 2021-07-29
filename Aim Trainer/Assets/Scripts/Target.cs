using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public GameObject circle;
    void Start(){
        Destroy(gameObject, 1.0f);
    }

    private void OnMouseDown() {
        GameManager.GetInstance().TargetHitted();
        GameManager.GetInstance().UpdateScore(1);
        Instantiate(circle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}