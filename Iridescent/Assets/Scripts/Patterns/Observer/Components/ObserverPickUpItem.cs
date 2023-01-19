using UnityEngine;

public class ObserverPickUpItem : MonoBehaviour, IObserver<int>
{
    [SerializeField]
    private AudioSource[] audios;


    private void Awake()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.AddObserver(this);
    }

    public void UpdateObserver(int data)
    {
        audios[data]?.Play();
    }
}