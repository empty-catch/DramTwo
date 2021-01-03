public class ClockSkill : INormalSkill {
    private const int SpCost = 2;
    
    public GestureType GestureType => GestureType.Clock;
    
    public void Activate() {
        if (PlayerCharacterController.Instance.Sp < SpCost) {
            throw new NotEnoughSpException("The player character's SP is not enough");
        }
        
        // TODO: 플레이어 제외 시간 50% 느려짐. (BGM도 느려짐)
    }
}