using System;
using System.Collections;
using System.Collections.Generic;
using Tempus.CoroutineTools;
using UnityEngine;

public class SkillHandler : MonoBehaviour {
    [SerializeField]
    private int specialSkillCooldown = 30;

    private readonly Dictionary<GestureType, ISkill> skills = new Dictionary<GestureType, ISkill>();

    private int specialSkillLevel;
    private int gestureCount;
    private bool canActivateSpecialSkill = true;

    public void Activate(GestureType gestureType) {
        if (skills.TryGetValue(gestureType, out var skill)) {
            skill.Activate();
        }
    }

    public void ActivateSpecialSkill() {
        if (canActivateSpecialSkill == false || gestureCount < 100) {
            gestureCount++;
            return;
        }

        gestureCount = 0;
        StartCoroutine(Cooldown());
        // TODO: 특수 스킬 사용
        "특수 스킬 사용됨".Log();
    }

    private IEnumerator Cooldown() {
        canActivateSpecialSkill = false;
        yield return Yield.Seconds(specialSkillCooldown);
        canActivateSpecialSkill = true;
    }

    private void Awake() {
        AddNormalSkill(new LightningSkill());
        AddNormalSkill(new HeartSkill());
        AddNormalSkill(new ClockSkill());
        AddNormalSkill(new StarSkill());
    }

    private void AddNormalSkill(ISkill skill) {
        skills.Add(skill.GestureType, skill);
    }
}