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

    public int currency;
    public Text currencyText;

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
        SetMaxHealth(maxHealth);
        currency = 0;
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
            if (!CheckDeath())
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

    public void AddCurrency(int amount)
    {
        currency += amount;
        currencyText.text = currency.ToString();
    }

    public bool BuyWithCurrency(int cost)
    {
        if (cost <= currency)
        {
            AddCurrency(-cost);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(player);
            health = 0;
            return true;
        }
        return false;
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
        healthSlider.value = health;
        if (health < 0)
        {
            healthText.text = "0/" + maxHealth.ToString();
        }
        else
        {
            healthText.text = healthText.text = Mathf.Ceil(health).ToString() + "/" + maxHealth.ToString();
        }
    }

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
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
