using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerCharacterController : SingletonObject<PlayerCharacterController> {
    public event Action<float> TimerFilled;
    public event Action<int, bool> GestureActiveSet;
    public event Action<int, Sprite> GestureSpriteSet;
    public event Action SpecialSkillApplied;

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
    private bool usingSpecialSkill;
    private readonly Queue<GestureType> gesturesToMatch = new Queue<GestureType>();

    public bool IsFullHp => hp >= MaxPoint;

    public int Sp { get; private set; } = MaxPoint;

    public void ProcessGesture(GestureType gestureType) {
        if (usingSpecialSkill && gestureType == gesturesToMatch.Peek()) {
            gesturesToMatch.Dequeue();

            if (gesturesToMatch.Count == 0) {
                // UI 정리 및 트윈 제거 해야함
                usingSpecialSkill = false;
                SpecialSkillApplied?.Invoke();
            }
        }

        if (gestureType >= GestureType.Lightning) {
            if (usingSpecialSkill == false) {
                skillDrawn?.Invoke(gestureType);
            }
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

    public void ApplyGracePeriod(int seconds) {
        isGracePeriod = true;
        DOVirtual.DelayedCall(seconds, () => isGracePeriod = false);
    }

    public void Foo() {
        var allGestures = Enum.GetValues(typeof(GestureType));

        for (var i = 0; i < GestureCount; i++) {
            var gesture = (GestureType) allGestures.GetValue(Random.Range(1, allGestures.Length));
            gesturesToMatch.Enqueue(gesture);
            GestureActiveSet?.Invoke(i, true);
            gesture.Log();
        }

        var fillAmount = 1f;
        usingSpecialSkill = true;

        DOTween.To(() => fillAmount, value => fillAmount = value, 0f, TimerDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() => TimerFilled?.Invoke(fillAmount))
            .OnComplete(() => usingSpecialSkill = false);
    }
}