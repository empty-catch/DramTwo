using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    [SerializeField]
    private UnityEvent destroyEvent;
    
    private void Awake() {
        hpItems = gameObject.GetComponentsInChildren<MonsterHpItem>(true);
        
        var imageCanvas = gameObject.GetComponentInChildren<Canvas>();
        if (imageCanvas.worldCamera == null) {
            imageCanvas.worldCamera = Camera.main;
        }
    }

    public void ActiveMonster() {
        gameObject.SetActive(true);
        // TODO : Random 제스쳐들 설정하기
    }

    private void OnEnable() {
        ResetObject();
        generateEvent?.Invoke();
    }

    public virtual void Move() {
        var direction = (PlayerCharacterController.instance.gameObject.transform.position -
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
    
    // TODO : 문양 갱신 만들기
    
    public void Death() {
        gameObject.SetActive(true);
        destroyEvent?.Invoke();
    }
    
    public virtual void ResetObject() {
        speed = defaultSpeed;
        hp = defaultHp;
    }
}
