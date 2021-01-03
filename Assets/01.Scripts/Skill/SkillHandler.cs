using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SkillHandler : MonoBehaviour {
    private readonly Dictionary<GestureType, ISkill> skills = new Dictionary<GestureType, ISkill>();
    private readonly Stopwatch cooldown = new Stopwatch();

    private int specialSkillLevel = 0;

    public void Activate(GestureType gestureType) {
        skills[gestureType]?.Activate();
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