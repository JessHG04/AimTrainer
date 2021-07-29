using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public GameObject circle;
    void Start(){
        //Destroy(gameObject, InitialOptionsWindow.GetInstance().GetDestroyTime());
    }

    private void OnMouseDown() {
        GameManager.GetInstance().TargetHitted();
        float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Debug.Log(distance);
        
        GameManager.GetInstance().UpdateScore(1);
        Instantiate(circle, transform.position, Quaternion.identity);
        //Destroy(gameObject);
    }
    
}