using System.Collections;
using UnityEngine;

public class Player : GObject
{
    public float attackCooldown;
    public bool onAttack;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackCD = attackCooldown;
        moveComponent = new PlayerMoveComponent(this);
        player = this;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move(this.gameObject, Time.fixedDeltaTime);

    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space)&&!onAttack){
            StartCoroutine(attack());
        }
    }

    protected IEnumerator attack()
    {
        onAttack = true;
        yield return new WaitForSeconds(attackCooldown);
        onAttack = false;
    }



}