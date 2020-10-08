using System;
using System.Collections.Generic;
using PDollarGestureRecognizer;
using UnityEngine;

public class GestureDrawer : MonoBehaviour {
    [SerializeField] private new LineRenderer renderer;
    [SerializeField] private TextAsset[] xmls;

    private Gesture[] gestures;
    private readonly List<Point> points = new List<Point>();
    private Vector3 position;
    private int strockID = -1;
    private int positionCount;

    private void Awake() {
        gestures = new Gesture[xmls.Length];
        for (var i = 0; i < xmls.Length; i++) {
            gestures[i] = GestureIO.ReadGestureFromXML(xmls[i].text);
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            strockID++;
            renderer.gameObject.SetActive(true);
            positionCount = 0;
        }
        else if (Input.GetMouseButton(0)) {
            position = Input.mousePosition;
            points.Add(new Point(position.x, -position.y, strockID));
            positionCount++;
            renderer.positionCount = positionCount;
        }
        else if (Input.GetMouseButtonUp(0)) {
            if (positionCount > 10 || (positionCount > 1 &&
                                       (renderer.GetPosition(0) - renderer.GetPosition(positionCount - 1))
                                       .sqrMagnitude >= 0.5f)) {
                var candidate = new Gesture(points.ToArray());
                var result = PointCloudRecognizer.Classify(candidate, gestures);
            }

            strockID = -1;
            points.Clear();
            renderer.positionCount = 0;
            renderer.gameObject.SetActive(false);
        }
    }
}