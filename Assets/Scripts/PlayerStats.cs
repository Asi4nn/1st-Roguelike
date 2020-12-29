using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;

    public GameObject player;
    public Text healthText;
    public Slider healthSlider;

    public float health;
    public float maxHealth;

    private bool isInvincible = false;
    [SerializeField] float invincibilityTime;
    [SerializeField] float invincibilityDeltaTime;

    private void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(float damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            UpdateHealth();
            CheckDeath();
            if (player != null)
            {
                StartCoroutine(BecomeTempInvincible());
            }
        }
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        UpdateHealth();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(player);
            health = 0;
        }
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private float CalculateHealthPercent()
    {
        return health / maxHealth;
    }

    private void UpdateHealth()
    {
        healthSlider.value = CalculateHealthPercent();
        if (health < 0)
        {
            healthText.text = "0/" + maxHealth.ToString();
        }
        else
        {
            healthText.text = healthText.text = Mathf.Ceil(health).ToString() + "/" + maxHealth.ToString();
        }
    }

    IEnumerator BecomeTempInvincible()
    {
        isInvincible = true;
        for (float i = 0; i < invincibilityTime; i += invincibilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (player.transform.localScale == Vector3.one)
            {
                ScalePlayer(Vector3.zero);
            }
            else
            {
                ScalePlayer(Vector3.one);
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        ScalePlayer(Vector3.one);
        isInvincible = false;
    }

    private void ScalePlayer(Vector3 scale)
    {
        player.transform.localScale = scale;
    }
}
