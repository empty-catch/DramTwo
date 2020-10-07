using UnityEngine;
using UnityEngine.UI;

public class MonsterHpItem : MonoBehaviour {
    private Image image;
    // TODO : 제스쳐 enum 받아와서 설정해주기 
    private void Awake() {
        image = gameObject.GetComponent<Image>();
    }

    public void OnEnable() {
           
    }

    
    // TODO : 제스쳐 받아와서 설정
    public void SettingGesture() {
        
    }

    public void ResetObject() {
        gameObject.SetActive(false);
    }
}