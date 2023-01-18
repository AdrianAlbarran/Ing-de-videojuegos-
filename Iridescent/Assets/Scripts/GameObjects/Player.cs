using System.Collections;
using UnityEngine;

public class Player : GObject
{
    public float attackSpeed;
    public bool onAttack;

    private Vector2 directionFacing;

    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackCD = attackSpeed;
        moveComponent = new PlayerMoveComponent(this);
        player = this;
        animator = GetComponent<Animator>();
        recieveAttackComponent = new RecieveAttackComponent(this);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move(this.gameObject, Time.fixedDeltaTime);
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movement_vector != Vector2.zero)
        {
            directionFacing = movement_vector;
        }        
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !onAttack)
        {
            StartCoroutine(attack());
        }
    }

    protected IEnumerator attack()
    {
        onAttack = true;
        RayAttack();
        yield return new WaitForSeconds(attackSpeed);
        onAttack = false;
    }

    public void RayAttack()
    {
        Vector2 diagonalUp = new Vector2(directionFacing.x, directionFacing.y + 1);
        Vector2 diagonalDown = new Vector2(directionFacing.x, directionFacing.y - 1);

        ContactFilter2D filtro = new ContactFilter2D();
        filtro.useLayerMask = true;
        filtro.layerMask = 1 << LayerMask.NameToLayer("Enemy");

        if (Physics2D.Raycast(transform.position, directionFacing, 10f))
        {
            RaycastHit2D[] hit = new RaycastHit2D[10];
            for (int i = 0; i < Physics2D.Raycast(transform.position, directionFacing, filtro, hit, 10f); i++)
            {
                hit[i].transform.GetComponent<Enemy>().RecieveAttack(dmg);
            }
        }
        if (Physics2D.Raycast(transform.position, diagonalUp, 10f))
        {
            RaycastHit2D[] hit = new RaycastHit2D[10];
            for (int i = 0; i < Physics2D.Raycast(transform.position, diagonalUp, filtro, hit, 10f); i++)
            {
                hit[i].transform.GetComponent<Enemy>().RecieveAttack(dmg);
            }
        }
        if (Physics2D.Raycast(transform.position, diagonalDown, 10f))
        {
            RaycastHit2D[] hit = new RaycastHit2D[10];
            for (int i = 0; i < Physics2D.Raycast(transform.position, diagonalDown, filtro, hit, 10f); i++)
            {
                hit[i].transform.GetComponent<Enemy>().RecieveAttack(dmg);
            }
        }
    }
}