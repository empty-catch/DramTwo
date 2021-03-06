using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour {
    [SerializeField]
    private BaseMonster targetMonsterPrefab;

    private Queue<BaseMonster> createdMonsterList = new Queue<BaseMonster>();
    
    public bool CreateMonster(out BaseMonster monster) {
        if (createdMonsterList.Peek() == null) {
            CreateNewMonster();
        }

        var monsterItem = createdMonsterList.Dequeue();
        monsterItem.DestroyAction = () => {
            createdMonsterList.Enqueue(monsterItem);
        };

        monster = monsterItem;
        return true;
    }

    private void CreateNewMonster() {
        var newMonster = Instantiate(targetMonsterPrefab, gameObject.transform);
        newMonster.gameObject.SetActive(false);
        
        createdMonsterList.Enqueue(newMonster);
    }
}
