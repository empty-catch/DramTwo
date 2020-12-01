using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : SingletonObject<MonsterGenerator> {
    [Header("Generate Transform")]
    [SerializeField]
    private Transform[] generateTransforms;

    public void GenerateMonster(MonsterFactory monsterFactory, int positionIndex = -1) {
        positionIndex 
            = positionIndex != -1 
            ? positionIndex 
            : Random.Range(0, generateTransforms.Length);
        
        BaseMonster createdMonster;
        
        if (monsterFactory.CreateMonster(out createdMonster)) {
            createdMonster.gameObject.transform.position = generateTransforms[positionIndex].position;
            createdMonster.ActiveMonster();
        }
    }
}