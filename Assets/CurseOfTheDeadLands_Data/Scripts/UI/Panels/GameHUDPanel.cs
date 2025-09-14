using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDPanel : MonoBehaviour
{
    public Image healthIndicator;
    public Image dangerIndicator;

    public void UpdateHealth()
    {
        healthIndicator.gameObject.SetActive(true);
        healthIndicator.GetComponent<DOTweenAnimation>().DORestart();
    }

    public void DisplayDanger()
    {
        dangerIndicator.gameObject.SetActive(true);
    }
}
