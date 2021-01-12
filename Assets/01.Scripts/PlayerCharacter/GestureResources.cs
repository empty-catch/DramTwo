using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gesture Resources", menuName = "Scriptable Object/Gesture Resources", order = 0)]
public class GestureResources : ScriptableObject {
    #region Singleton

    private static GestureResources instance;

    public static GestureResources Instance {
        get {
            if (instance != null) {
                return instance;
            }

            instance = Resources.Load<GestureResources>("Unit/Resource/Gesture Resources");
            return instance;
        }
    }

    #endregion

    [SerializeField]
    private Info[] infos;

    private Dictionary<GestureType, Sprite> items = new Dictionary<GestureType, Sprite>();

    public IEnumerable<GestureType> AllGestures => items.Keys;
    public Sprite Sprite(GestureType gesture) => items[gesture];

    private void OnEnable() {
        foreach (var info in infos) {
            items[info.gesture] = info.sprite;
        }
    }
    
    [Serializable]
    private struct Info {
        public GestureType gesture;
        public Sprite sprite;
    }
}