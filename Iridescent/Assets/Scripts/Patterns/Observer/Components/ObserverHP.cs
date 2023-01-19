using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObserverHP : MonoBehaviour, IObserver<int>
{   
    private Player player;
    public TextMeshProUGUI text;
    
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.AddObserver(this);
    }

    public void UpdateObserver(int data)
    {
        text.text = player.getHp()+"/"+player.getMaxHp();
    }
}
