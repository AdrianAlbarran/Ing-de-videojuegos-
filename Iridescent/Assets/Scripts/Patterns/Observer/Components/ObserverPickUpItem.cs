using UnityEngine;

public class ObserverPickUpItem : MonoBehaviour, IObserver<int>
{



    private void Awake()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.AddObserver(this);
    }

    public void UpdateObserver(int data)
    {
        switch (data)
        {
            case 0:
                AudioManager.instance.Play("DmgUp");
                break;
            case 1:
                AudioManager.instance.Play("HealingUP");
                break;
            case 2:
                AudioManager.instance.Play("ASpeedUP");
                break;
            case 3:
                AudioManager.instance.Play("SpeedUP");
                break;
            case 4:
                AudioManager.instance.Play("PlayerScream");
                break;


        }
    }
}