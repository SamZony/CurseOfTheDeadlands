using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TurnOnAfterCinematics : MonoBehaviour
{
    public PlayableDirector timeline;
    public List<GameObject> objects;

    // Turn on the objects after the timeline finishes
    void Start()
    {
        if (timeline != null)
        {
            timeline.stopped += OnTimelineStopped;
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        foreach (var obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
        gameObject.SetActive(false);
    }
}
