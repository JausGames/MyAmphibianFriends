using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHands : MonoBehaviour
{
    [SerializeField] Vector3 attackPoint;
    [SerializeField] Transform hands;
    private void Awake()
    {
        hands = transform;
    }
    void Update()
    {
        hands.position = attackPoint;
    }

    public void SetPoint(Vector3 pos)
    {
        attackPoint = pos;
    }
}
