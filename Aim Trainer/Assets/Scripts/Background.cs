using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour{
    private void OnMouseDown(){
        if(GameManager.getInstance().getState() == GameManager.State.Playing){
            GameManager.getInstance().loseLife();
        }
    }
}
