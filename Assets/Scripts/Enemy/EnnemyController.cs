using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnnemyController : MonoBehaviour
{
    [SerializeField] float lookRadius = 15f;
    [SerializeField] float attackRadius = 1.3f;
    [SerializeField] float attackTimer = 0.4f;
    [SerializeField] bool canMove = true;

    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] EnemyHands hands;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 attackPoint;
    [SerializeField] private float timeInRange = 0f;
    [SerializeField] private float startTimer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //aim setter needed
        agent = GetComponent<NavMeshAgent>();
        hands = GetComponentInChildren<EnemyHands>();
        startPosition = transform.position;
        GetRandom();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
           if (startTimer <= 2f)
        {
            agent.SetDestination(target.position - direction * 1.3f);
            direction = (target.position - transform.position).normalized;
            attackPoint = transform.position + direction * 1.3f;
            hands.SetPoint(attackPoint);
            startTimer += Time.deltaTime;
            return;
        }
        if (agent.speed < 10f) agent.speed += 0.05f * Time.deltaTime;

        //Set target
        var distances = new List<float>();
        if (PlayerManager.instance.alive.Count == 0) return;
        foreach (Player ply in PlayerManager.instance.alive)
        {
            distances.Add(Vector3.Distance(transform.position, ply.transform.position));
        }
        var closest = distances.Min();
        int index = distances.IndexOf(closest);
        target = PlayerManager.instance.alive[index].transform;
        
        direction = (target.position - transform.position).normalized;
        attackPoint = transform.position + direction * attackRadius;
        hands.SetPoint(attackPoint);
        float distance = Vector3.Distance(target.position, transform.position + Vector3.forward * attackRadius);

        if (distance <= lookRadius)
        {
             agent.SetDestination(target.position - direction * attackRadius);
            if (distance >= attackRadius * 3)
            {
                FaceTarget();
            }
        }
    }
    private void FixedUpdate()
    {
        if (!canMove) return;
        float distance = Vector3.Distance(target.position, attackPoint);
        if (distance <= attackRadius)
        {
            timeInRange += Time.deltaTime;
            if (timeInRange >= attackTimer) Attack(target.gameObject.GetComponent<Player>());
        }
        else
        {
            timeInRange = 0;
        }
    }

    public void FaceTarget()
    {
        Debug.Log("FaceTarget : " + target.position);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawSphere(transform.position, lookRadius);
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(attackPoint, attackRadius);
    }
    void Attack(Player player)
    {
        player.GetHit(1f);
    }
    
    public void ResetPosition()
    {
        transform.position = startPosition;
        agent.SetDestination(startPosition);
        hands.transform.position = startPosition;
        hands.SetPoint(startPosition);
        GetRandom();
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
    public void GetRandom()
    {
        int nbPlayer = PlayerManager.instance.players.Count;
        Debug.Log("Number Player : " + nbPlayer);
        var number = Random.Range(0, nbPlayer - 1);
        Debug.Log("Number : " + number);
        target = PlayerManager.instance.players[number].transform;
        startTimer = 0;
    }
}
