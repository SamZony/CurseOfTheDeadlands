using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

/// <summary>
/// Continuously updates HDRP camera Depth of Field focus distance
/// using a raycast from the camera toward the target, 
/// so the focus locks to the hit point on colliders.
/// </summary>
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(HDAdditionalCameraData))]
public class HDRP_AutoFocusRaycast : MonoBehaviour
{
    [Tooltip("The object the camera should look at (ray is cast in this direction).")]
    public Transform focusTarget;

    [Tooltip("Maximum raycast distance.")]
    public float maxDistance = 100f;

    [Tooltip("Optional layers to restrict what the raycast can hit.")]
    public LayerMask focusLayers = ~0;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (focusTarget == null)
            return;

        Vector3 origin = cam.transform.position;
        Vector3 direction = (focusTarget.position - origin).normalized;

        // Perform raycast
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, focusLayers))
        {
            // Focus exactly on the collider surface
            cam.focusDistance = hit.distance;
        }
        else
        {
            // If nothing hit, fall back to distance to target
            cam.focusDistance = Vector3.Distance(origin, focusTarget.position);
        }
    }
}
