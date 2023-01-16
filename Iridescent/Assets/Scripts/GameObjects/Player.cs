using System.Collections;
using UnityEngine;

public class Player : GObject
{
    public float attackCooldown;
    public bool onAttack;
    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackCD = attackCooldown;
        moveComponent = new PlayerMoveComponent(this);
        player = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move(this.gameObject, Time.fixedDeltaTime);
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
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