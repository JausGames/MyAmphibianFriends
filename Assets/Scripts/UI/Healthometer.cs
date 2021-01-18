using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthometer : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private Player player;
    

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        setHealth();    
    }

    private float GetHealth()
    {
        return player.GetHealth();
    }

    public void setMaxHealth(float value)
    {
        slider.maxValue = value;
    }

    public void setHealth()
    {
        slider.value = GetHealth();
    }
    public void setPlayer(Player pl)
    {
        player = pl;
    }
}
