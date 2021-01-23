public class CircleSkill : INormalSkill {
    private const int SpCost = 2;

    public GestureType GestureType => GestureType.Circle;

    public void Activate() {
        if (PlayerCharacterController.Instance.Sp < SpCost) {
            throw new NotEnoughSpException("The player character's SP is not enough");
        }

        PlayerCharacterController.Instance.ApplyProtection(1);
        // TODO: 방어 성공 시 Sp 1 회복
    }
}