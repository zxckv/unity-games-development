using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public enum SoundType {
        Click,
        Purchase,
        Pistol,
        Rifle,
        Reload,
        Zombie,
        Oven,
        Drink,
        Hit,
        Shotgun,
        Submachinegun
    }

    [System.Serializable]
    public class Sound {
        public SoundType type;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1f;

        [HideInInspector]
        public AudioSource source;
    }

    public static AudioManager Instance;

    public Sound[] AllSounds;
    private Dictionary<SoundType, Sound> _SoundDict = new Dictionary<SoundType, Sound>();

    private void Awake() {
        Instance = this;

        foreach(var s in AllSounds) { _SoundDict[s.type] = s; }
    }

    void Update() {
    }

    public void Play(SoundType type) {
        if (!_SoundDict.TryGetValue(type, out Sound s)) {
            Debug.LogWarning($"Sound type {type} not found.");
            return;
        }

        var soundObj = new GameObject($"Sound_{type}");
        var audioSrc = soundObj.AddComponent<AudioSource>();

        audioSrc.clip = s.clip;
        audioSrc.volume = s.volume;

        audioSrc.Play();

        Destroy(soundObj, s.clip.length);
    }
}
