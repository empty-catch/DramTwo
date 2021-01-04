using System.Collections;
using Tempus.CoroutineTools;
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
    private bool isGracePeriod;

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

    public void Heal() {
        if (hp != MaxPoint) {
            hp++;
        }
    }

    public IEnumerator ApplyGracePeriodCoroutine(int seconds) {
        isGracePeriod = true;
        yield return Yield.Seconds(seconds);
        isGracePeriod = false;
    }
}