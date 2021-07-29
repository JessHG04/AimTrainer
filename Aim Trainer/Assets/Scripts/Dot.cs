using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour{
    private void Start(){
        Destroy(gameObject, InitialOptionsWindow.GetInstance().GetDestroyTime());
    }
}
