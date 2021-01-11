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
    public event Action<GestureType> GestureDrawn;
    public event Action<GestureType> SkillDrawn;

    [SerializeField]
    private int gestureCount;

    [SerializeField]
    private int maxPoint;

    [SerializeField]
    private float timerDuration;

    [SerializeField]
    private float moveSpeed;

    private int hp;
    private bool isGracePeriod;
    private bool usingSpecialSkill;
    private readonly Queue<GestureType> gesturesToMatch = new Queue<GestureType>();

    public bool IsFullHp => hp >= maxPoint;
    public int GestureCount => gestureCount;

    public int Sp { get; private set; }

    private void Awake() {
        hp = maxPoint;
        Sp = maxPoint;
    }

    public void ProcessGesture(GestureType gestureType) {
        if (usingSpecialSkill && gestureType == gesturesToMatch.Peek()) {
            gesturesToMatch.Dequeue();

            if (gesturesToMatch.Count == 0) {
                // TODO: UI 정리 및 트윈 제거 해야함
                usingSpecialSkill = false;
                SpecialSkillApplied?.Invoke();
            }
        }

        if (gestureType >= GestureType.Lightning) {
            if (usingSpecialSkill == false) {
                SkillDrawn?.Invoke(gestureType);
            }
        }
        else {
            GestureDrawn?.Invoke(gestureType);
        }
    }

    public void Heal() {
        if (hp != maxPoint) {
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

        DOTween.To(() => fillAmount, value => fillAmount = value, 0f, timerDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() => TimerFilled?.Invoke(fillAmount))
            .OnComplete(() => {
                usingSpecialSkill = false;
                FailurePenalty();
            });
    }

    private void FailurePenalty() {
        if (Random.Range(0, 2) == 0) {
            hp = 1;
        }
        else {
            if (Sp == 0) {
                // TODO: 그림 그리기 3초동안 봉인
            }
            else {
                Sp = 0;
            }
        }
    }
}