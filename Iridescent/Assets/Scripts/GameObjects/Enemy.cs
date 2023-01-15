using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GObject
{
    [SerializeField]
    private int moveSpeed;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        moveComponent = new EnemyMoveComponent(this, moveSpeed);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move(this.gameObject, Time.fixedDeltaTime);
    }
}
