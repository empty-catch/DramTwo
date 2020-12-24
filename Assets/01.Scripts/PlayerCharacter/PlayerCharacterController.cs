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

    [SerializeField]
    private UnityEvent<GestureType> gestureDrawn;

    [SerializeField]
    private UnityEvent<GestureType> skillDrawn;

    public void ProcessGesture(GestureType gestureType) {
        if (gestureType >= GestureType.Lightning) {
            skillDrawn?.Invoke(gestureType);
        }
        else {
            gestureDrawn?.Invoke(gestureType);
        }
    }
}