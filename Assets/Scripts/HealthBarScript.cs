using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    [SerializeField]private Image healthBarSprite;
    private float fillAmount;
    [SerializeField]private Camera cam;
    [SerializeField]private float reduceSpeed = 2;
    private void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, fillAmount, reduceSpeed*Time.deltaTime);
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
    public void UpdateHealthBar(float maxHealth,float currentHealth)
    {
        fillAmount = currentHealth / maxHealth;
        if(fillAmount<0.51f)
        {
            healthBarSprite.color = Color.orange;
        } else if(fillAmount<0.26f)
        {
            healthBarSprite.color = Color.red;
        }
        else
        {
            healthBarSprite.color = Color.green;
        }
    }
}
