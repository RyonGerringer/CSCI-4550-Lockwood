using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;

    [HideInInspector]
    public Player character;

    public Image meterImage;

    public Text hpText;

    float maxHitPoints;

    //gets the max hit points from inspector
    void Start()
    {
        maxHitPoints = character.maxHitPoints;
    }
    //Updates the HealthBar with the amount of hit points
    void Update()
    {
        if (character != null)
        {
            meterImage.fillAmount = hitPoints.value / maxHitPoints;

            hpText.text = "HP:  " + (meterImage.fillAmount * 100);
        }
    }
}
