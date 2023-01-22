using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : GObject,ISubject<int>
{
    public float attackSpeed;
    public float ms;
    public bool onAttack;
    

    public List<GameObject> enemiesHit;

    private Vector2 directionFacing;

    private Animator animator;

    private int maxHP;
    public int getMaxHp()
    {
        return maxHP;
    }

    private int lastDir;
    private int soundObserver;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackCD = attackSpeed;
        moveComponent = new PlayerMoveComponent(this);
        player = this;
        animator = GetComponent<Animator>();
        recieveAttackComponent = new RecieveAttackComponent(this);
        drops = GameObject.Find("DropsManager").GetComponent<PrototypeTester>();
        maxHP = hp;
        pauseMenu = GameObject.Find("GameManager").GetComponent<PauseMenu>();
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
        changeLastDir();
    }



    public void Update()
    {
        if (!pauseMenu.gamePaused)
        {
            if (Input.GetKey(KeyCode.Space) && !onAttack)
            {
                animator.SetBool("Attack", true);
            }
        }
        ms = moveComponent.Getms();
        if (hp <= 0)
        {
            Die(this.gameObject);
            Destroy(this.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }

    protected void changeLastDir()
    {

        // Ya lo siento por las ramificaciones, se que apestan

        if (directionFacing.x == 1)
        {
            lastDir = 1;
        }
        else if (directionFacing.x == -1)
        {
            lastDir = 3;
        }
        else if (directionFacing.y == 1)
        {
            lastDir = 0;
        }
        else if (directionFacing.y == -1)
        {
            lastDir = 2;
        } 
        animator.SetInteger("LastDir", lastDir);
    }

    protected void attackChange()
    {
        animator.SetBool("Attack", false);
    }

    protected void attack()
    {
        StartCoroutine(auxAttack());
    }

    protected IEnumerator auxAttack()
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

        Debug.DrawRay(transform.position, directionFacing *2.3f, Color.red, 10f);

        if (Physics2D.Raycast(transform.position, directionFacing, 2.3f))
        {
            RaycastHit2D[] hit = new RaycastHit2D[10];
            for (int i = 0; i < Physics2D.Raycast(transform.position, directionFacing, filtro, hit, 2.3f); i++)
            {
                hit[i].transform.GetComponent<Enemy>().RecieveAttack(dmg);
                enemiesHit.Add(hit[i].transform.gameObject);
                StartCoroutine(frezeeEnemy(hit[i].transform.GetComponent<Enemy>()));

            }
        }
        if (Physics2D.Raycast(transform.position, diagonalUp, 2.1f))
        {
            RaycastHit2D[] hit = new RaycastHit2D[10];
            for (int i = 0; i < Physics2D.Raycast(transform.position, diagonalUp, filtro, hit, 2.1f); i++)
            {
                if (!enemiesHit.Contains(hit[i].transform.gameObject))
                {
                    hit[i].transform.GetComponent<Enemy>().RecieveAttack(dmg);
                    enemiesHit.Add(hit[i].transform.gameObject);
                    StartCoroutine(frezeeEnemy(hit[i].transform.GetComponent<Enemy>()));
                }
            }
        }
        if (Physics2D.Raycast(transform.position, diagonalDown, 2.1f))
        {
            RaycastHit2D[] hit = new RaycastHit2D[10];
            for (int i = 0; i < Physics2D.Raycast(transform.position, diagonalDown, filtro, hit, 2.1f); i++)
            {
                if (!enemiesHit.Contains(hit[i].transform.gameObject))
                {
                    hit[i].transform.GetComponent<Enemy>().RecieveAttack(dmg);
                    enemiesHit.Add(hit[i].transform.gameObject);
                    StartCoroutine(frezeeEnemy(hit[i].transform.GetComponent<Enemy>()));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "Damage":
                dmg += 5;
                soundObserver = 0;
                break;

            case "Health":
                maxHP += 25;
                hp = maxHP;
                soundObserver = 1;
                break;
            case "AttackSpeed":
                attackSpeed *= 0.8f;
                soundObserver = 2;
                break;
            case "MoveSpeed":
                moveComponent.Setms(0.5f);
                soundObserver = 3;
                break;
        }
        NotifyObservers();

        drops.Pickup(collision.gameObject.transform.position);
        Destroy(collision.gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag=="Enemies" && !waitAttack)
        {
            StartCoroutine(attackCooldown());
            soundObserver = 4;
            NotifyObservers();
        }
    }


    private List<IObserver<int>> _observers = new List<IObserver<int>>();
    public void AddObserver(IObserver<int> observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver<int> observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IObserver<int> observer in _observers)
        {
            observer?.UpdateObserver(soundObserver);
        }
    }

    public IEnumerator frezeeEnemy(Enemy enemigo)
    {
        enemigo.hit(true);
        yield return new WaitForSeconds(0.5f);
        enemigo.hit(false);
    }
}