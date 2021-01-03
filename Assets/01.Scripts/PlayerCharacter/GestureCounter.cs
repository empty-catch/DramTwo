using System;
using System.Collections.Generic;

public static class GestureCounter {
    private static readonly Dictionary<GestureType, int> counter = new Dictionary<GestureType, int>();

    public static int UsedCount(this GestureType gestureType) {
        return counter.ContainsKey(gestureType) == false ? 0 : counter[gestureType];
    }

    public static void Count(GestureType gestureType) {
        if (counter.ContainsKey(gestureType) == false) {
            counter.Add(gestureType, 0);
        }

        counter[gestureType]++;
    }
}