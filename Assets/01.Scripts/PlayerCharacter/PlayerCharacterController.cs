using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    private const float TimerDuration = 4f;

    [SerializeField]
    private UnityEvent playerDead;

    [SerializeField]
    private UnityEvent<GestureType> gestureDrawn;

    [SerializeField]
    private UnityEvent<GestureType> skillDrawn;

    private int hp = MaxPoint;
    private bool isGracePeriod;
    private readonly Queue<GestureType> gesturesToMatch = new Queue<GestureType>();

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
        var allGestures = Enum.GetValues(typeof(GestureType));

        for (var i = 0; i < GestureCount; i++) {
            var gesture = (GestureType) allGestures.GetValue(Random.Range(0, allGestures.Length));
            gesturesToMatch.Enqueue(gesture);
            GestureActiveSet?.Invoke(i, true);
        }

        var fillAmount = 1f;
        DOTween.To(() => fillAmount, value => fillAmount = value, 0f, TimerDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() => TimerFilled?.Invoke(fillAmount));
    }
}