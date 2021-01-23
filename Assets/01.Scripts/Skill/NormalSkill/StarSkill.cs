public class StarSkill : INormalSkill {
    private const int SpCost = 2;

    public GestureType GestureType => GestureType.Star;

    public void Activate() {
        if (PlayerCharacterController.Instance.Sp < SpCost) {
            throw new NotEnoughSpException("The player character's SP is not enough");
        }

        // TODO: 조건을 몬스터가 필드에 없을 때로 변경
        if (false) {
            throw new SkillCannotBeUsedException("There are no monsters on the field");
        }

        // TODO: 필드 위 랜덤 몬스터 4마리 3초 기절
        SpecialSkillLevel.UpdateLevel(3);
    }
}