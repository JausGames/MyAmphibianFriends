using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isAlive = true;
    [SerializeField] private float health = 200f;
    [SerializeField] private float playerIndex = 0;
    [SerializeField] public PlayerController controller = null;
    [SerializeField] public Healthometer healthometer = null;
    [SerializeField] public Transform visual;
    [SerializeField] public PlayerParticleManager particles;
    // Start is called before the first frame update
    void Awake()
    {
        visual = transform.Find("Visual");
        particles = visual.GetComponentInChildren<PlayerParticleManager>();
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (transform.position.y < -5) Die();
    }

    public bool GetAlive()
    {
        return isAlive;
    }
    public void SetAlive(bool value)
    {
        isAlive = value;
    }
    public float GetHealth()
    {
        return health;
    }
    public void ResetHealth()
    {
        health = 200f;
    }
    public bool GetHit(float damage)
    {
        if (health > damage) { health -= damage; return true; }
        
        Die();
        return false;
    }
    private void Die()
    {
        health = 0f;
        isAlive = false;
        controller.SetCanMove(false);
    }
    public void SetPlayerIndex(int value)
    {
        playerIndex = value;
        controller.SetPlayerIndex(value);
    }
    public void StopMotion()
    {
        controller.StopMotion();
    }
    public void AddHealth(float value)
    {
        health += value;
        particles.PlayHealthParticle();
    }

}
