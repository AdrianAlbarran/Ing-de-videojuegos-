using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveAttackComponent : IRecieveAttackComponent
{
    private GObject gObject;
    public RecieveAttackComponent(GObject _gObject)
    {
        gObject = _gObject;
    }
    public void RecieveAttack(int dmg)
    {
       if(gObject.getHp()-dmg<=0)
        {
            gObject.setHP(0);
        }
        else
        {
            gObject.setHP(gObject.getHp()-dmg);
        }
    }

}
