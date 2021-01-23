public class AdrenalineSkill : ISpecialSkill {
    public int RequireLevel => 0;

    public void Activate() {
        PlayerCharacterController.Instance.AdjustMoveSpeed(1.5f, 10f);
    }
}