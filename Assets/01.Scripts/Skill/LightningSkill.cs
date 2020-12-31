using UnityEngine;

public class LightningSkill : ISkill {
    private const int SpCost = 2;
    public GestureType GestureType => GestureType.Lightning;

    public void Activate() {
        if (PlayerCharacterController.Instance.Sp < SpCost) {
            throw new NotEnoughSpException("The player character's SP is not enough");
        }

        // TODO: 모든 몬스터 모양 1개씩 제거
    }
}