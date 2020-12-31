using UnityEngine;

public class HeartSkill : ISkill {
    private const int SpCost = 3;
    public GestureType GestureType => GestureType.Heart;

    public void Activate() {
        if (PlayerCharacterController.Instance.Sp < SpCost) {
            throw new NotEnoughSpException("The player character's SP is not enough");
        }

        if (PlayerCharacterController.Instance.IsFullHp) {
            throw new SkillCannotBeUsedException("The player character's HP is full");
        }

        PlayerCharacterController.Instance.Hp++;
    }
}