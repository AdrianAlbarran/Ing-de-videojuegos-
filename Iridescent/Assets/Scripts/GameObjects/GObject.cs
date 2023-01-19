using UnityEngine;

public abstract class GObject : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;

    public IMoveComponent moveComponent;
    public IDieComponent dieComponent;
    public IRecieveAttackComponent recieveAttackComponent;

    protected PauseMenu pauseMenu;

    [HideInInspector]
    public Player player;

    [HideInInspector]
    public float attackCD;

    [SerializeField]
    protected int hp;

    [SerializeField]
    protected int dmg;

    protected PrototypeTester drops;

    public void setHP(int _hp)
    {
        hp = _hp;
    }

    public int getHp()
    {
        return hp;
    }


    public void Move(GameObject gameObject, float deltaTime)
    {
        moveComponent?.Move(gameObject, deltaTime);
    }

    public void Die(GameObject gameObject)
    {
        dieComponent?.Die(gameObject);
    }
    public void RecieveAttack(int dmg)
    {
        recieveAttackComponent?.RecieveAttack(dmg);
    }
}