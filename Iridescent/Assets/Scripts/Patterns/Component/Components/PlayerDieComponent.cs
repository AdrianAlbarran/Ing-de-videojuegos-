using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieComponent : IDieComponent
{
    public PlayerDieComponent(GameObject gameObject)
    {

    }

    public void Die(GameObject gameObject)
    {
        Debug.Log("Perdiste pendejo");
        Application.Quit();
    }

}
