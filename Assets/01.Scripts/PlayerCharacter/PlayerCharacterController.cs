using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacterController : MonoBehaviour {
    #region Singleton

    private static PlayerCharacterController instance;

    public static PlayerCharacterController Instance {
        get {
            instance ??= FindObjectOfType<PlayerCharacterController>();
            if (instance != null) {
                return instance;
            }

            var prefabs = Resources.Load<GameObject>("/Unit/PlayerCharacter");
            var playerCharacterObject = Instantiate(prefabs, Vector2.zero, Quaternion.identity);
            instance = playerCharacterObject.GetComponentSafe<PlayerCharacterController>();

            return instance;
        }
    }

    #endregion

    private const int MaxPoint = 6;

    [SerializeField]
    private UnityEvent playerDead;

    [SerializeField]
    private UnityEvent<GestureType> gestureDrawn;

    [SerializeField]
    private UnityEvent<GestureType> skillDrawn;

    private int hp = MaxPoint;
    private int sp = MaxPoint;

    public bool IsFullHp => Hp >= MaxPoint;

    public int Hp {
        get => hp;
        set {
            hp = Mathf.Clamp(value, 0, MaxPoint);
            if (hp == 0) {
                playerDead?.Invoke();
            }
        }
    }

    public int Sp {
        get => sp;
        set => sp = Mathf.Clamp(value, 0, MaxPoint);
    }

    public void ProcessGesture(GestureType gestureType) {
        if (gestureType >= GestureType.Lightning) {
            skillDrawn?.Invoke(gestureType);
        }
        else {
            gestureDrawn?.Invoke(gestureType);
        }
    }
}