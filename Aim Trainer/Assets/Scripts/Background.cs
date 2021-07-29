using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour{
    private void OnMouseDown(){
        if(GameManager.GetInstance().GetState() == GameManager.State.Playing){
            GameManager.GetInstance().LoseLife();
        }
    }
}
