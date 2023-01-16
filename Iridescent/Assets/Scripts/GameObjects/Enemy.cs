using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GObject
{
    [SerializeField]
    private int moveSpeed;

    [SerializeField]
    private int attackSpeed;
    private int drop;

    
    protected void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        moveComponent = new EnemyMoveComponent(this, moveSpeed);
        dieComponent = new EnemyDieComponent();
        recieveAttackComponent = new RecieveAttackComponent(this);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move(this.gameObject, Time.fixedDeltaTime);
        if (hp <= 0) Die(this.gameObject);
    }

}
