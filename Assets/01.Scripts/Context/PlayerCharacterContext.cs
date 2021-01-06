using Slash.Unity.DataBind.Core.Data;

public class PlayerCharacterContext : Context {
    private const int GestureCount = 5;

    private Property<bool[]> gestureActiveProperty = new Property<bool[]>();
    private Property<float> timerFillProperty = new Property<float>();

    public PlayerCharacterContext() {
        GestureActive = new bool[GestureCount];
    }

    public bool[] GestureActive {
        get => gestureActiveProperty.Value;
        private set => gestureActiveProperty.Value = value;
    }

    public float TimerFill {
        get => timerFillProperty.Value;
        private set => timerFillProperty.Value = value;
    }
}