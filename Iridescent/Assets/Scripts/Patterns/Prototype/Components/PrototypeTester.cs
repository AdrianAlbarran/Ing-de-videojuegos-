using UnityEngine;

public class PrototypeTester : MonoBehaviour
{
    public IPrototypeDrop[] _drops;
    private DmgDrop _dmgDrop;
    private HealthDrop _healthDrop;
    private AttackSpeedDrop _asDrop;
    private MoveSpeedDrop _msDrop;
    private const int NUMBER_OF_DROPS = 10;
    //private GameObject dmgDropPrefab = Resources.Load<GameObject>("Prefabs/DmgDrop");

    // Start is called before the first frame update
    private void Awake()
    {
        _drops = new IPrototypeDrop[NUMBER_OF_DROPS];
        _dmgDrop = new DmgDrop();
        _healthDrop = new HealthDrop();
        _asDrop = new AttackSpeedDrop();
        _msDrop = new MoveSpeedDrop();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        for (int i = 0; i < _drops.Length; i++)
        {
            if (_drops[i] != null)
            {
                if (_drops[i].IsAlive())
                {
                    _drops[i].Render();
                }
            }
        }
    }

    public void AddDrop(Vector3 position)
    {
        for (int i = 0; i < _drops.Length; i++)
        {
            if (_drops[i] == null)
            {
                int random = Random.Range(0, 4);
                switch (random)
                {
                    case 0:
                        _drops[i] = _dmgDrop.Clone(position);
                        break;

                    case 1:
                        _drops[i] = _healthDrop.Clone(position);
                        break;
                    case 2:
                        _drops[i] = _asDrop.Clone(position);
                        break;
                    case 3:
                        _drops[i] = _msDrop.Clone(position);
                        break;
                }
                break;
            }
        }
    }

    public void Pickup(Vector3 position)
    {
        for (int i = 0; i < _drops.Length; i++)
        {
            if (_drops[i] != null)
            {
                if (_drops[i].IsAlive() && _drops[i].GetPosition() == position)
                {
                    _drops[i].SetAlive(false);
                    _drops[i] = null;
                }
            }
        }
    }
}