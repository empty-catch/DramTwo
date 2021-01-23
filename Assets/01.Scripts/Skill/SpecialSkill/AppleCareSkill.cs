public class AppleCareSkill : ISpecialSkill {
    public int RequireLevel => 2;

    public void Activate() {
        PlayerCharacterController.Instance.Heal(150);
        PlayerCharacterController.Instance.ApplyProtection();
    }
}