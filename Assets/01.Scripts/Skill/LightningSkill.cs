using UnityEngine;

public class LightningSkill : ISkill {
    public GestureType GestureType => GestureType.Lightning;
    
    public void Activate() {
        // TODO: 모든 몬스터 모양 1개씩 제거
    }
}