using UnityEngine;

public class ClockSkill : ISkill {
    private const int SpCost = 3;
    
    public GestureType GestureType => GestureType.Clock;
    
    public void Activate() {
        if (PlayerCharacterController.Instance.Sp < SpCost) {
            throw new NotEnoughSpException("The player character's SP is not enough");
        }
        
        
    }
}