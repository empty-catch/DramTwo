using UnityEngine;
using UnityEngine.UI;

public class MonsterHpItem : MonoBehaviour {
    private Image image;

    private void Awake() {
        image = gameObject.GetComponent<Image>();
    }

    public void SettingGesture(GestureType key) {
        image.sprite = MonsterGestureResources.Instance.GestureItems[key];
    }

    public void ResetObject() {
        gameObject.SetActive(false);
    }
}