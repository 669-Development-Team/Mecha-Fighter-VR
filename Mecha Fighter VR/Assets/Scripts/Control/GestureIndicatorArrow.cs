using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureIndicatorArrow : MonoBehaviour, GestureListener
{
    public bool isLeft;
    public GestureHandlerV2 gestureHandler;

    private GestureTrackingNode trackingNode;

    private static Color minColor = Color.red;
    private static Color maxColor = Color.green;

    private Transform arrow;
    private Transform arrowTarget;
    private Material arrowMaterial;

    private static float maxDisplacementMagnitude = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        arrow = transform.GetChild(0);
        arrowMaterial = arrow.GetChild(0).GetComponent<MeshRenderer>().material;
        arrowTarget = transform.GetChild(1);

        gestureHandler.addGestureConfirmListener(this);
    }

    private bool gestureConfirmed = false;
    private float confirmAnimationTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        trackingNode = gestureHandler.GetTrackingNode(isLeft);

        // usually the confirm "animation" will play for half a second after the gesture is confirmed
        // but if the tracker starts recording again within that time we stop animating
        if (trackingNode.currentlyRecording()) gestureConfirmed = false;

        if (!gestureConfirmed) {

            var displacementDir = trackingNode.currentGestureDirection();
            var displacementMagnitude = Mathf.Min(maxDisplacementMagnitude, trackingNode.currentGestureMagnitude() * 0.1f);

            if (displacementMagnitude == 0) {
                arrow.gameObject.SetActive(false);
                return;
            }
            else {
                arrow.gameObject.SetActive(true);
            }

            arrowTarget.localPosition = displacementDir;
            arrow.localScale = new Vector3(0.05f, 0.05f, displacementMagnitude);
            arrow.LookAt(arrowTarget);

            arrowMaterial.SetColor("_BaseColor", Color.Lerp(minColor, maxColor, displacementMagnitude / maxDisplacementMagnitude));
        }
        else {
            arrow.localScale = new Vector3(0.05f, 0.05f, maxDisplacementMagnitude);
            arrowMaterial.SetColor("_BaseColor", maxColor);
        }
    }

    public void OnGestureConfirm() {
        gestureConfirmed = true;
        StartCoroutine(ConfirmGesture());
    }

    IEnumerator ConfirmGesture() {
        yield return new WaitForSeconds(confirmAnimationTime);
        gestureConfirmed = false;
    }
}
