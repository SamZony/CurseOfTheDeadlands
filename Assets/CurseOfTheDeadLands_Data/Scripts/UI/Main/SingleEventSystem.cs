using UnityEngine;
using UnityEngine.EventSystems;

public class SingleEventSystem : MonoBehaviour
{
    public enum InstanceMode
    {
        KeepThis,
        KeepOther
    }

    [Tooltip("Choose whether this instance survives or the existing ones do")]
    public InstanceMode mode = InstanceMode.KeepThis;

    private static SingleEventSystem instance;

    void Awake()
    {
        // Collect all EventSystems in the scene
        var allEventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);

        if (allEventSystems.Length > 1)
        {
            if (mode == InstanceMode.KeepThis)
            {
                // Destroy all others and keep this
                foreach (var es in allEventSystems)
                {
                    if (es.gameObject != gameObject)
                        Destroy(es.gameObject);
                }
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (mode == InstanceMode.KeepOther)
            {
                // Keep the first found, destroy this
                Destroy(gameObject);
            }
        }
        else
        {
            // Only one EventSystem exists, so keep it
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
