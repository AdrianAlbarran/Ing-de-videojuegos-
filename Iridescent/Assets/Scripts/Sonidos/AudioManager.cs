using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager instance;

    public Slider sliderGeneral, sliderMusic, sliderSFX;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(this); Por si tenemos musica y queremos cambiar de escenay no se resetee

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;

            s.source.dopplerLevel = s.dopplerLevel;

            s.source.spatialBlend = s.spatialBlend;

            s.source.rolloffMode = s.volumeRolloff;

            s.source.minDistance = s.minDistance;

            s.source.maxDistance = s.maxDistance;

            if (s.audioType == Sound.AudioTypes.SoundEffect)
                s.source.outputAudioMixerGroup = SoundsEffectsMixerGroup;
            else if (s.audioType == Sound.AudioTypes.Music)
                s.source.outputAudioMixerGroup = MusicMixerGroup;
            else
                s.source.outputAudioMixerGroup = GeneralMixerGroup;

            if (s.playOnAwake)
                s.source.Play();
        }
    }



    private void Start()
    {
        //InilitializeVolumen();
    }

    //Inicializa los volumenes segun los ajustes del jugador o al maximo si no los ha tocado
    private void InilitializeVolumen()
    {
        sliderGeneral.value = PlayerPrefs.GetFloat("SliderGeneral", 1.0f);
        sliderMusic.value = PlayerPrefs.GetFloat("SliderMusic", 1.0f);
        sliderSFX.value = PlayerPrefs.GetFloat("SliderSFX", 1.0f);
        GeneralMixerGroup.audioMixer.SetFloat("Master", Mathf.Log10(sliderGeneral.value) * 20);
        GeneralMixerGroup.audioMixer.SetFloat("Music", Mathf.Log10(sliderMusic.value) * 20);
        GeneralMixerGroup.audioMixer.SetFloat("SFX", Mathf.Log10(sliderSFX.value) * 20);
    }

    #endregion Singleton

    [Tooltip("Lista de los sonidos que se van a usar")]
    public Sound[] sounds;

    [Space(10)]
    [Header("Mixers")]
    [SerializeField]
    [Tooltip("Grupo del mixer que servira para subir o bajar el volumen general")]
    private AudioMixerGroup GeneralMixerGroup;

    [SerializeField]
    [Tooltip("Grupo del mixer que servira para subir o bajar el volumen de la musica")]
    private AudioMixerGroup MusicMixerGroup;

    [SerializeField]
    [Tooltip("Grupo del mixer que servira para subir o bajar el volumen de los efectos de sonido")]
    private AudioMixerGroup SoundsEffectsMixerGroup;

    //Inicia el sonido x
    /// <summary>
    /// Plays a sound.
    /// </summary>
    /// <param name="name">Name of the sound</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    //Para el sonido x
    /// <summary>
    /// Stops playing a sound.
    /// </summary>
    /// <param name="name">Name of the sound</param>
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound " + s.name + " not found!");
            return;
        }
        s.source.Stop();
    }

    //Detecta si el sonido x esta sonando
    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound " + s.name + " not found!");
            return false;
        }
        if (s.source.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Metodo que controla el volumen general atraves del slider y lo guarda
    public void GeneralSound(float sliderValue)
    {
        GeneralMixerGroup.audioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SliderGeneral", sliderValue);
        PlayerPrefs.Save();
    }

    //Metodo que controla el volumen sfx atraves del slider y lo guarda
    public void SFXSound(float sliderValue)
    {
        SoundsEffectsMixerGroup.audioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SliderSFX", sliderValue);
        PlayerPrefs.Save();
    }

    //Metodo que controla el volumen musica atraves del slider y lo guarda
    public void MusicSound(float sliderValue)
    {
        SoundsEffectsMixerGroup.audioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SliderMusic", sliderValue);
        PlayerPrefs.Save();
    }

    //Metodo para resetear los ajustes
    public void ResetAjustes()
    {
        sliderGeneral.value = 1;
        sliderMusic.value = 1;
        sliderSFX.value = 1;
    }
}