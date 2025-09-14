using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pos = other.transform.position;
            string posString = pos.x + "," + pos.y + "," + pos.z;
            PlayerPrefs.SetString(GameManager.Instance.checkpointPlayerPref, posString);
            PlayerPrefs.Save();
        }
    }

}
