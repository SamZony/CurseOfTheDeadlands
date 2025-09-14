using UnityEngine;

public class vChangeAnimatorUpdateMode : MonoBehaviour
{
    public Animator animator;
    private readonly AnimatorUpdateMode initialState = AnimatorUpdateMode.Fixed;

    public void Reset()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        if (!animator) animator = GetComponentInParent<Animator>();
    }

    public void ChangeToUnscaledTime()
    {
        if (Time.timeScale == 0)
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void ChangeToAnimatePhysics()
    {
        animator.updateMode = AnimatorUpdateMode.Fixed;
    }

    public void ChangeToInitialState()
    {
        animator.updateMode = initialState;
    }
}
