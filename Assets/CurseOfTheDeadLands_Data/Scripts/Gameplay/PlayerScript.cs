using DG.Tweening;
using GameUI;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            MainUICanvas.Instance.gameHUDPanel.dangerIndicator.GetComponent<DOTweenAnimation>().DORestart();    
        }
    }

    private void Start()
    {
        GameManager.Instance.ShowHUD();
    }
}
