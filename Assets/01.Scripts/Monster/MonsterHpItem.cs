using UnityEngine;
using UnityEngine.UI;

public class MonsterHpItem : MonoBehaviour {
    private Image image;
    // TODO : 제스쳐 enum 받아와서 설정해주기 
    private void Awake() {
        image = gameObject.GetComponent<Image>();
    }

    // TODO : enum으로 변경
    public void SettingGesture(string key) {
        image.sprite = MonsterGestureResources.instance.GestureItems[key];
    }

    public void ResetObject() {
        gameObject.SetActive(false);
    }
}