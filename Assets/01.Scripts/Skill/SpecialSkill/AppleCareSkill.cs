public class AppleCareSkill : ISpecialSkill {
    public int RequireLevel => 2;

    public void Activate() {
        PlayerCharacterController.Instance.Heal(3);
        PlayerCharacterController.Instance.ApplyProtection();
    }
}