using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : SingletonObject<MonsterGenerator> {
    [Header("Generate Transform")]
    [SerializeField]
    private MonsterSpawnPoint[] monsterSpawnPoint;

    public void GenerateMonster(MonsterFactory monsterFactory, int positionIndex = -1) {
        positionIndex 
            = positionIndex != -1 
            ? positionIndex 
            : Random.Range(0, monsterSpawnPoint.Length);
        
        BaseMonster createdMonster;
        
        if (monsterFactory.CreateMonster(out createdMonster)) {
            monsterSpawnPoint[positionIndex].SetMonsterSpeed(createdMonster);
            createdMonster.gameObject.transform.position = monsterSpawnPoint[positionIndex].gameObject.transform.position;
            createdMonster.ActiveMonster();
        }
    }
}