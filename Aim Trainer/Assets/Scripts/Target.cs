using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    void Start(){
        Destroy(gameObject, 1.0f);
    }

    private void OnMouseDown() {
        GameManager.getInstance().targetHitted();
        GameManager.getInstance().updateScore(1);
        Destroy(gameObject);
    }
}