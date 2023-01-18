using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeTester : MonoBehaviour
{
    private IPrototypeDrop[] _drops;
    private DmgDrop _dmgDrop;
    private const int NUMBER_OF_DROPS =1;
    //private GameObject dmgDropPrefab = Resources.Load<GameObject>("Prefabs/DmgDrop");
    
    // Start is called before the first frame update
    void Awake()
    {
        _drops = new IPrototypeDrop[NUMBER_OF_DROPS];
        _dmgDrop = new DmgDrop();
        _drops[0] = _dmgDrop.Clone(new Vector3(0,0,0));
        _drops[0].SetAlive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < _drops.Length; i++)
        {
            if (_drops[i].IsAlive())
            {  
                _drops[i].Render();
            }
        }
    }

    public void AddDrop(Vector3 position)
    {
        for (int i = 0; i < _drops.Length; i++)
        {
            if (!_drops[i].IsAlive())
            {
                _drops[i] = _dmgDrop.Clone(position);
                break;
            }
        }
    }
}
