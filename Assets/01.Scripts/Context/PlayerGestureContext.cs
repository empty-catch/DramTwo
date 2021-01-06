using Slash.Unity.DataBind.Core.Data;

public class PlayerGestureContext : Context {
    private Property<bool[]> gestureActiveProperty = new Property<bool[]>();

    public PlayerGestureContext() {
        GestureActive = new bool[SkillHandler.Instance.GestureCount];
    }

    public bool[] GestureActive {
        get => gestureActiveProperty.Value;
        private set => gestureActiveProperty.Value = value;
    }
}