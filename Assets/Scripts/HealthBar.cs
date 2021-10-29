using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    [SerializeField] private Color healthFullColor = Color.red, emptyHealthColor = Color.grey;

    public void SetHealth(int currentHp)
    {
        healthBar.fillAmount = currentHp;
    }
    
    public void SetHealth(float currentHp)
    {
        healthBar.fillAmount = currentHp;
    }
    //Set the HealthBar Color based on the value of the current Health
    public void SetHealthColor(float currentHp)
    {
        healthBar.color = Color.Lerp(emptyHealthColor, healthFullColor, currentHp);
    }
}
