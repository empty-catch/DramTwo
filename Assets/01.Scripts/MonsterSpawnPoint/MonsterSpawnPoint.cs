using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour {
   [SerializeField]
   private int speedCoefficient;

   public void SetMonsterSpeed(BaseMonster monster) {
      monster.Speed *= speedCoefficient;
   }
}
