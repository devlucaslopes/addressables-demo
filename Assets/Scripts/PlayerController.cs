using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed;

    private Rigidbody2D body;

    void Start()
    {
        body= GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        body.velocity = new Vector2(Speed * direction, body.velocity.y);
    }
}
