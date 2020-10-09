using System;
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

    public Gesture Gesture { get; private set; }
    public GestureType Type => type;
    public Color Color => color;

    private void OnEnable() {
        Gesture = GestureIO.ReadGestureFromXML(xml.text);
    }
}