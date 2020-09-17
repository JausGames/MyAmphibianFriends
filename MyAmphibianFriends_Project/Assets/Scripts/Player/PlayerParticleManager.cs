using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem healthBonus;
    // Start is called before the first frame update
    void Start()
    {
        healthBonus.Stop();
    }

    public void PlayHealthParticle()
    {
        healthBonus.Play();
    }
}
