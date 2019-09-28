using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpdateHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private Destroyable player;

    private void Start()
    {
        healthBar.maxValue = player.health;
        healthBar.value = player.health;
    }

    public void UpdateBar(int health)
    {
        healthBar.value = health;
    }
}
