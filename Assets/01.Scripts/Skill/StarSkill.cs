using UnityEngine;

public class StarSkill : ISkill {
    public GestureType GestureType => GestureType.Star;
    
    public void Activate() {
        // TODO: 필드 위 모든 몬스터 3초 기절
    }
}