using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRow : MonoBehaviour
{
    //A reference to the keyboard animation script in the parent object
    private KeyboardAnimation keyboardAnimation;

    //How long it takes to complete the translation
    private const float translateTime = 0.2f;
    //The distance the row should move each frame to translate in the given time
    private Vector3 distPerFrame;
    //The target position
    private Vector3 targetPosition;
    //True if the row is moving and false if the row it at its target position
    private bool translating = false;

    //How long it takes to complete scaling
    private const float scaleTime = 0.2f;
    //The amount the row should scale each frame to scale in the given time
    private Vector3 scalePerFrame;
    //The target scale
    private Vector3 targetScale;
    //True if the row is scaling and false if the row it at its target scale
    private bool scaling = false;

    //The default attributes of the row
    private Vector3 defaultPosition;
    private Vector3 defaultScale;

    void Start()
    {
        keyboardAnimation = gameObject.GetComponentInParent<KeyboardAnimation>();
        defaultPosition = gameObject.transform.localPosition;
        defaultScale = gameObject.transform.localScale;
    }

    //Update the position and scale of the row
    void FixedUpdate()
    {
        updatePosition();
        updateScale();
    }

    //Displace the row
    public void displaceRow(Vector3 targetDisplacement)
    {
        targetPosition = defaultPosition + targetDisplacement;
        distPerFrame = (targetPosition - gameObject.transform.localPosition) / (translateTime * 60f);
        translating = true;
    }

    //Scale the row
    public void scaleRow(float scaleFactor)
    {
        targetScale = defaultScale * scaleFactor;
        scalePerFrame = (targetScale - gameObject.transform.localScale) / (scaleTime * 60f);
        scaling = true;
    }

    //Determine if adding the displacement to the current value would overshoot the target
    private bool overShoot(float current, float target, float displacement)
    {
        if (current < target && current + displacement >= target || current > target && current + displacement <= target)
            return true;

        return false;
    }

    private void updatePosition()
    {
        //If the row it not at its target position, update the position
        if (translating)
        {
            Vector3 currentPos = gameObject.transform.localPosition;

            //If updating the position would overshoot the target, set the position to the target and stop translating
            if (overShoot(currentPos.x, targetPosition.x, distPerFrame.x)
                || overShoot(currentPos.y, targetPosition.y, distPerFrame.y)
                || overShoot(currentPos.z, targetPosition.z, distPerFrame.z))
            {
                gameObject.transform.localPosition = targetPosition;
                translating = false;
            }
            //Otherwise update the position
            else
                gameObject.transform.localPosition = currentPos + distPerFrame;
        }
    }

    public void updateScale()
    {
        //If the row it not at its target scale, update the scale
        if (scaling)
        {
            Vector3 currentScale = gameObject.transform.localScale;

            //If updating the scale would overshoot the target, set the scale to the target and stop scaling
            if (overShoot(currentScale.x, targetScale.x, scalePerFrame.x))
            {
                gameObject.transform.localScale = targetScale;
                scaling = false;
            }
            //Otherwise update the scale
            else
                gameObject.transform.localScale = currentScale + scalePerFrame;
        }
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        displaceRow(new Vector3(0, 0, -0.05f));
        scaleRow(1.1f);
    }

    private void OnTriggerExit(Collider otherObject)
    {
        displaceRow(new Vector3(0, 0, 0));
        scaleRow(1f);
    }
}
