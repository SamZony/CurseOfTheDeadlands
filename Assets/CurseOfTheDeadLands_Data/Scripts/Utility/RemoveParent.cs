using UnityEngine;

public class RemoveParent : MonoBehaviour
{
    private void Awake()
    {
        transform.DetachChildren();
    }
}
