﻿using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SoundSystemDef {
    public string soundName;
    public AudioClip clip;
}

public class SoundSystem : MonoBehaviour {
    public int numberOfSources = 20;
    public AudioSource audioSourcePrefab;

    public SoundSystemDef[] soundLibrary = null;
    public SoundSystemDef[] bgmLibrary = null;

    private int current = 0;
    private AudioSource[] sources;

    private AudioSource backgroundMusic;

    public float musicVolume = 1.0f;
    public float effectVolume = 1.0f;

    private Dictionary<string, AudioClip> soundMap = new Dictionary<string, AudioClip>(); 
    private Dictionary<string, AudioClip> bgmMap = new Dictionary<string, AudioClip>(); 

    void Awake() {
        for (int i = 0; i < bgmLibrary.Length; i++) {
            SoundSystemDef def = bgmLibrary[i];
            bgmMap[def.soundName] = def.clip;
        }
        for (int i = 0; i < soundLibrary.Length; i++) {
            SoundSystemDef def = soundLibrary[i];
            soundMap[def.soundName] = def.clip;
        }

        sources = new AudioSource[numberOfSources];
        for (int i = 0; i < numberOfSources; i++) {
            AudioSource source = Instantiate(audioSourcePrefab, transform.position, transform.rotation) as AudioSource;
            source.transform.parent = this.transform;
            sources[i] = source;
        }
        backgroundMusic = Instantiate(audioSourcePrefab, transform.position, transform.rotation) as AudioSource;
        backgroundMusic.transform.parent = this.transform;
    }

    void Start() {
        Object.DontDestroyOnLoad(this.gameObject);
    }

    private AudioSource GetNextSource() {
        AudioSource source = sources[current];
        current = (current + 1) % numberOfSources;
        return source;
    }

    public void StopBackgroundMusic() {
      backgroundMusic.Stop();
      backgroundMusic.volume = 0;
    }

    public void PlayBackgroundMusicNoStatus(string bgmName) {
        PlayBackgroundMusic(bgmName);
    }

    public int PlayBackgroundMusic(string bgmName) {
      if (!this.enabled) {
          return 0;
      }
      if (backgroundMusic == null) {
        Util.Log("WTF");
        return -1;
      }

      if (!bgmMap.ContainsKey(bgmName)) {
          Util.Log("Tried to play nonexistent music {0}", bgmName);
          return -1;
      }

      backgroundMusic.clip = bgmMap[bgmName];
      backgroundMusic.loop = true;
      backgroundMusic.Play();

      return 0;
    }

    public void StopSound(string soundName) {
        if (!this.enabled) {
            return;
        }

        if (!soundMap.ContainsKey(soundName)) {
            Util.Log("Tried to stop nonexistent sound {0}", soundName);
            return;
        }

        foreach (AudioSource a in sources) {
            if (a.clip == soundMap[soundName])
              if (a.isPlaying)
                a.Stop();
        }
    }

    public void PlaySoundNoStatus(string soundName) {
        PlaySound(soundName);
    }

    public int PlaySound(string soundName) {
        if (!this.enabled) {
            return 0;
        }

        if (!soundMap.ContainsKey(soundName)) {
            Util.Log("Tried to play nonexistent sound {0}", soundName);
            return -1;
        }

        AudioClip clip = soundMap[soundName];
        AudioSource source = GetNextSource();
        source.clip = clip;
        source.loop = false;

        source.Play();

        return current;
    }
}
