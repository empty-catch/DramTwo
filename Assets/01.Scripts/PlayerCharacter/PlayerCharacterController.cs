using System;
using System.Collections;
using Tempus.CoroutineTools;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerCharacterController : SingletonObject<PlayerCharacterController> {
    public event Action<float> TimerFilled;
    public event Action<int, bool> GestureActiveSet;
    public event Action<int, Sprite> GestureSpriteSet;

    public const int GestureCount = 5;
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

    public void Foo() {
        var gestures = new GestureType[GestureCount];
        var g = Enum.GetValues(typeof(GestureType));

        for (var i = 0; i < GestureCount; i++) {
            gestures[i] = (GestureType) g.GetValue(Random.Range(0, g.Length));
            GestureActiveSet?.Invoke(i, true);
        }

        TimerFilled?.Invoke(1f);
    }
}