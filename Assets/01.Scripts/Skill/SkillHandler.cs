using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SkillHandler : MonoBehaviour {
    private readonly Dictionary<GestureType, ISkill> skills = new Dictionary<GestureType, ISkill>();
    private readonly Stopwatch cooldown = new Stopwatch();

    private int specialSkillLevel = 0;

    public void Activate(GestureType gestureType) {
        if (skills.TryGetValue(gestureType, out var skill)) {
            skill.Activate();
        }
        else {
            throw new ArgumentException("There is no skill corresponding to the gesture");
        }
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