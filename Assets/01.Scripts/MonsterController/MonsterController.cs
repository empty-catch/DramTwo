using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : SingletonObject<MonsterController> {
    
    private HashSet<BaseMonster> activeMonsters = new HashSet<BaseMonster>();

    public void SubscribeActiveMonster(BaseMonster monster) {
        activeMonsters.Add(monster);
    }

    public void UnsubscribeActiveMonster(BaseMonster monster) {
        if (activeMonsters.Contains(monster)) {
            activeMonsters.Remove(monster);
        }
    }

    public void ApplyEffectToAllMonster(Action<HashSet<BaseMonster>> effectAction) {
        effectAction?.Invoke(activeMonsters);
    }
}
