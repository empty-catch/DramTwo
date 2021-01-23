using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonsterAttackFactory {
    public static IMonsterAttack CreateMonsterAttack(MonsterAttackType type) {
        switch (type) {
            case MonsterAttackType.Melee:
                return new MonsterMeleeAttack();
                break;
        }

        return null;
    }
}