using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GObject
{
    public float attackSpeed;
    public bool onAttack;

    public List<GameObject> enemiesHit;

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
            animator.SetBool("Attack", true);
            Debug.Log("attack");
            StartCoroutine(attack());
        }
    }

    protected void attackChange()
    {
        animator.SetBool("Attack", false);
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
        enemiesHit.Clear();
        Vector2 diagonalUp = new Vector2(0, 0);
        Vector2 diagonalDown = new Vector2(0, 0);
        if (directionFacing.y == 0)
        {
            diagonalUp = new Vector2(directionFacing.x, directionFacing.y + 1);
            diagonalDown = new Vector2(directionFacing.x, directionFacing.y - 1);
        }
        else if (directionFacing.x == 0)
        {
            diagonalUp = new Vector2(directionFacing.x + 1, directionFacing.y);
            diagonalDown = new Vector2(directionFacing.x - 1, directionFacing.y);
        }
        else if ((directionFacing.x == 1 || directionFacing.x == -1) && (directionFacing.y == 1 || directionFacing.y == -1)) { }
        {
            directionFacing.y = 0;
            diagonalUp = new Vector2(directionFacing.x, directionFacing.y + 1);
            diagonalDown = new Vector2(directionFacing.x, directionFacing.y - 1); ;
        }

        ContactFilter2D filtro = new ContactFilter2D();
        filtro.useLayerMask = true;
        filtro.layerMask = 1 << LayerMask.NameToLayer("Enemies");

        if (Physics2D.Raycast(transform.position, directionFacing, 1))
        {
            RaycastHit2D[] hit = new RaycastHit2D[10];
            for (int i = 0; i < Physics2D.Raycast(transform.position, directionFacing, filtro, hit, 1); i++)
            {
                hit[i].transform.GetComponent<Enemy>().RecieveAttack(dmg);
                enemiesHit.Add(hit[i].transform.gameObject);
            }
        }
        if (Physics2D.Raycast(transform.position, diagonalUp, 0.9f))
        {
            RaycastHit2D[] hit = new RaycastHit2D[10];
            for (int i = 0; i < Physics2D.Raycast(transform.position, diagonalUp, filtro, hit, 0.9f); i++)
            {
                if (!enemiesHit.Contains(hit[i].transform.gameObject))
                {
                    hit[i].transform.GetComponent<Enemy>().RecieveAttack(dmg);
                    enemiesHit.Add(hit[i].transform.gameObject);
                }
            }
        }
        if (Physics2D.Raycast(transform.position, diagonalDown, 0.9f))
        {
            RaycastHit2D[] hit = new RaycastHit2D[10];
            for (int i = 0; i < Physics2D.Raycast(transform.position, diagonalDown, filtro, hit, 0.9f); i++)
            {
                if (!enemiesHit.Contains(hit[i].transform.gameObject))
                {
                    hit[i].transform.GetComponent<Enemy>().RecieveAttack(dmg);
                    enemiesHit.Add(hit[i].transform.gameObject);
                }
            }
        }
    }
}