using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMeleeAttack : IMonsterAttack {
    private int damage;
    
    public void Attack() {
        PlayerCharacterController.Instance.GetDamaged(damage);
    }
    
    /// <param name="args">int: damage</param>
    public void AttackSetting(params object[] args) {
        if (args.Length != 1 || args[0] is int == false) {
            throw new ArgumentException();
        }

        damage = (int)args[0];
    }
}
