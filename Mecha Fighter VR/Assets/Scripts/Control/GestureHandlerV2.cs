using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHandlerV2 : MonoBehaviour
{
    public GestureTrackingNode leftTracker;
    public GestureTrackingNode rightTracker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rightTracker.GetGesture(Gestures.UP) && !leftTracker.GripHeldDown())
        {

        }
    }

    IEnumerator awaitGesture(GestureTrackingNode tracker, Gestures gesture)
    {
        while (tracker.GripHeldDown())
        {
            yield return null;
        }

        if (tracker.GetGesture(gesture))
        {

        }
    }
}
