using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] float playerHeight;
    [SerializeField] protected float speed;

    protected Rigidbody rb;
    protected bool IsGrounded { get => Physics.Raycast(transform.position, Vector3.down, playerHeight); }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
