using Slash.Unity.DataBind.Foundation.Setters;
using UnityEngine;

public class GameObjectsActiveSetter : SingleSetter<bool[]> {
    [SerializeField]
    private GameObject[] gameObjects;

    protected override void OnValueChanged(bool[] newValue) {
        for (var i = 0; i < newValue.Length && i < gameObjects.Length; i++) {
            gameObjects[i].SetActive(newValue[i]);
        }
    }
}