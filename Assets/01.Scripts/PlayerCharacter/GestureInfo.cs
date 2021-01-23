#pragma warning disable CS0649

using PDollarGestureRecognizer;
using UnityEngine;

[CreateAssetMenu(fileName = "Gesture Info", menuName = "Scriptable Object/Gesture Info", order = 0)]
public class GestureInfo : ScriptableObject {
    [SerializeField]
    private TextAsset xml;

    [SerializeField]
    private GestureType type;

    [SerializeField]
    private Color color = Color.white;

    [SerializeField]
    private bool isUnlocked;

    public Gesture Gesture { get; private set; }
    public GestureType Type => type;
    public Color Color => color;
    public bool IsUnlocked => isUnlocked;

    private void OnEnable() {
        Gesture = GestureIO.ReadGestureFromXML(xml.text);
    }
}