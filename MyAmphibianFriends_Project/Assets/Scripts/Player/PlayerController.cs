using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform visual;
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform front;
    [SerializeField] private bool onFloor = false;

    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float jumpSpeed = 80f;
    [SerializeField] private bool canMove = false;
    [SerializeField] private float charging = 0.01f;
    [SerializeField] private float nb = 0f;
    [SerializeField] private float rotate = 0;
    [SerializeField] private bool jump = false;
    [SerializeField] private int jumpNumber = 2;
    void Awake()
    {
        visual = transform.Find("Visual");
        body = GetComponent<Rigidbody>();
        front = visual.transform.Find("Front");
    }
    public int GetPlayerIndex()
    {
        return playerIndex;
    }
    public void SetPlayerIndex(int nb)
    {
        playerIndex = nb;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walkable"))
        {
            Debug.Log("PlayerController, OnCollisionEnter : CollisionGameobjectLayer = " + collision.gameObject.layer);
            onFloor = true;
            jumpNumber = 2;
       };
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!canMove) return;


        Debug.Log("Jump & Charging : " + jump + " & " + charging);

        visual.Rotate(Vector3.up * rotate * Time.deltaTime * rotationSpeed, Space.Self);
        if (!jump && charging > 0.2f && jumpNumber != 0 && onFloor)
        {
            Debug.Log("PlayerController, Update : Jump 1");
            Jump((front.position - visual.position + 2f * Vector3.up) * jumpSpeed * charging);
        }
        if (!jump && charging > 0.2f && jumpNumber != 0)
        {
            Debug.Log("PlayerController, Update : Jump 2");
            Jump(((front.position - visual.position) * 1.7f + Vector3.up) * jumpSpeed * charging);
        }

        if (jump)
        {
            nb += Time.deltaTime;
            charging = 1.5f * nb / (nb + 0.3f) + 0.7f;
        }
        else if (charging > 0f)
        {
            nb -= Time.deltaTime;
            charging = 1.5f * nb / (nb + 0.3f) + 0.7f;
        }
        else
        {
            charging = 0f;
            nb = 0f;
        }
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
        if (value)
        {
            transform.localEulerAngles = Vector3.zero;
            body.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            body.constraints = RigidbodyConstraints.None;
        }
    }
    public bool GetCanMove()
    {
        return canMove;
    }
    public void SetRotate(float value)
    {
        rotate = value;
    }
    public void SetJump(bool value)
    {
        jump = value;
    }

    void Jump(Vector3 dir)
    {
        Debug.Log("Move : " + dir);
        StopMotion();
        body.AddForce(dir);
        charging = 0;
        onFloor = false;
        jumpNumber --;
    }
    public void StopMotion()
    {
        body.velocity = Vector3.zero;
    }
}
