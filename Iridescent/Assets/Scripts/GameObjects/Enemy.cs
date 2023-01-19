using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GObject
{
    [SerializeField]
    private int moveSpeed;

    [SerializeField]
    private int attackSpeed;

    private bool recivedAttack;

  

    
    protected void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        moveComponent = new EnemyMoveComponent(this, moveSpeed);
        dieComponent = new EnemyDieComponent();
        recieveAttackComponent = new RecieveAttackComponent(this);
        drops = GameObject.Find("DropsManager").GetComponent<PrototypeTester>();
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.mass = 1;
        rb.drag = 100;
        rb.angularDrag = 100;
        rb.freezeRotation = true;
        gameObject.AddComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!recivedAttack)
        {
            Move(this.gameObject, Time.fixedDeltaTime);
        }
 
        if (hp <= 0)
        {
            int random = Random.Range(1, 11);
            if(random <11) drops.AddDrop(this.transform.position);
            Die(this.gameObject);
        }
    }

    public void hit(bool value)
    {
        recivedAttack = value;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && !waitAttack)
        {
            StartCoroutine(attackCooldown());
            collision.gameObject.GetComponent<Player>().RecieveAttack(dmg);
        }
    }



}
