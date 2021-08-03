using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyDataSO", menuName = "Aim Trainer/DifficultyDataSO", order = 0)]

public class DifficultyDataSO : ScriptableObject {
    public List<DifficultyData> difficultyDataList = new List<DifficultyData> ();
}

[Serializable]
public class DifficultyData {
    public int lifes;
    public int targetsAmount;
    public float timeToSpawnTarget;
    public float timeToDestroyTarget;
    public float targetSpeed;
    public float timeToFinishGame;
}
