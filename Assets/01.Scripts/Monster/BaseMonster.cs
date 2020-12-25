using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public abstract class BaseMonster : MonoBehaviour {
    private MonsterHpItem[] hpItems;
        
    [Header("Values")]
    [SerializeField]
    private float defaultSpeed;
    
    private float speed;
    public float Speed {
        get => speed;
        set => speed = value;
    }

    [SerializeField]
    private int defaultHp;
    private int hp;
    public int Hp => hp;

    [Header("Events")]
    [SerializeField]
    private UnityEvent generateEvent;

    private Action generateAction;
    public Action GenerateAction {
        get => generateAction;
        set => generateAction = value;
    }
    
    [SerializeField]
    private UnityEvent destroyEvent;
    
    private Action destroyAction;
    public Action DestroyAction {
        get => destroyAction;
        set => destroyAction = value;
    }
    
    private void Awake() {
        hpItems = gameObject.GetComponentsInChildren<MonsterHpItem>(true);
        
        var imageCanvas = gameObject.GetComponentInChildren<Canvas>();
        if (imageCanvas.worldCamera == null) {
            imageCanvas.worldCamera = Camera.main;
        }
    }

    public void ActiveMonster() {
        gameObject.SetActive(true);
        AllocateNewGesture();
    }

    private void OnEnable() {
        ResetObject();
        generateEvent?.Invoke();
        generateAction?.Invoke();
    }

    public virtual void Move() {
        var direction = (PlayerCharacterController.Instance.gameObject.transform.position -
                        gameObject.transform.position).normalized;

        gameObject.transform.Translate(direction * (speed * Time.deltaTime));
    }

    public virtual void Attack() { }

    public virtual void GetDamaged(int damage) {
        hp -= damage;
        
        if ( hp <= 0) {
            Death();
        }
    }
    
    private void AllocateNewGesture() {
        List<GestureType> gestureTypes = new List<GestureType>();
        var monsterGestureItems = MonsterGestureResources.Instance.GestureItems;
        
        foreach (var item in monsterGestureItems) {
            gestureTypes.Add(item.Key);
        }
        
        if (gestureTypes.Count <= hp) {
            throw new Exception("Hp is bigger than enum gesture length.");
        }

        GestureType randomGesture;
        
        for (int i = 0; i < hp; i++) {
            randomGesture = (GestureType)Random.Range(0, gestureTypes.Count);
            gestureTypes.Remove(randomGesture);
            
            hpItems[i].SettingGesture(randomGesture);
        }
    }
    
    public void Death() {
        gameObject.SetActive(true);
        destroyEvent?.Invoke();
        destroyAction?.Invoke();

        generateAction = null;
        destroyAction = null;
    }
    
    public virtual void ResetObject() {
        speed = defaultSpeed;
        hp = defaultHp;
    }
}
