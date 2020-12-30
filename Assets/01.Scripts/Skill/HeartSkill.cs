using UnityEngine;

public class HeartSkill : ISkill {
    public GestureType GestureType => GestureType.Heart;
    
    public void Activate() {
        PlayerCharacterController.Instance.Hp++;
    }
}