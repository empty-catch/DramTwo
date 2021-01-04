using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacterController : SingletonObject<PlayerCharacterController> {
    private const int MaxPoint = 6;

    [SerializeField]
    private UnityEvent playerDead;

    [SerializeField]
    private UnityEvent<GestureType> gestureDrawn;

    [SerializeField]
    private UnityEvent<GestureType> skillDrawn;

    private int hp = MaxPoint;

    public bool IsFullHp => hp >= MaxPoint;

    public int Sp { get; private set; } = MaxPoint;

    public void ProcessGesture(GestureType gestureType) {
        if (gestureType >= GestureType.Lightning) {
            skillDrawn?.Invoke(gestureType);
        }
        else {
            gestureDrawn?.Invoke(gestureType);
        }
    }

    public void Heal(uint amount) {
        hp = Mathf.Clamp(hp + (int)amount, 0, MaxPoint);
    }
}