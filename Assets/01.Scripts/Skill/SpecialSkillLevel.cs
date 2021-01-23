public static class SpecialSkillLevel {
    private static readonly int[] levelUpRequirements = new int[] {150, 50, 50};
    private static int levelUpProgress;

    private static int LevelUpRequirement => levelUpRequirements[Value];
    public static int Value { get; private set; }

    public static void UpdateLevel(int targetLevel) {
        if (targetLevel != Value + 1) {
            return;
        }

        levelUpProgress++;

        if (levelUpProgress >= LevelUpRequirement) {
            levelUpProgress = 0;
            Value++;
        }
    }
}