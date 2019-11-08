using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Listens for changes in a specified state of the animator
public class AnimationListener
{
    private GameObject parent;
    private Animator animator;
    private int animationLayer;
    private string state;
    private System.Action onEnter;
    private System.Action onExit;
    private System.Action onStay;

    public AnimationListener(MonoBehaviour parent, string stateName, System.Action onEnter, System.Action onExit) : this(parent, stateName, 0, onEnter, onExit, null) { }

    public AnimationListener(MonoBehaviour parent, string stateName, int layer, System.Action onEnter, System.Action onExit, System.Action onStay)
    {
        this.animator = parent.GetComponent<Animator>();
        this.state = stateName;
        this.onEnter = onEnter;
        this.onExit = onExit;

        parent.StartCoroutine(Update());
    }

    private IEnumerator Update()
    {
        bool inState = false;

        while (true)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(animationLayer);

            bool tmp_inState = stateInfo.IsName(state);
            if (tmp_inState && !inState)
            {
                onEnter?.Invoke();
            }
            if (!tmp_inState && inState)
            {
                onExit?.Invoke();
            }
            if (tmp_inState && inState)
            {
                onStay?.Invoke();
            }

            inState = tmp_inState;

            yield return null;
        }
    }
}
