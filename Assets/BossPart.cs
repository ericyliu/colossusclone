using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPart : MonoBehaviour {

  public int health = 3;
  public bool alive = true;
  public AudioClip owClip;
  AudioSource audioSource;

  void Start() {
    audioSource = transform.GetComponent<AudioSource>();
  }

  void Update() {
    if (!alive) {
      Destroy(gameObject);
    }
  }

	public void ReceiveDamage() {
    health--;
    playSound(owClip);
    if (health <= 0) {
      alive = false;
    }
  }

  void playSound(AudioClip clip, bool loop = false) {
    if (audioSource.clip != clip || !loop) {
      audioSource.clip = clip;
      audioSource.loop = loop;
      audioSource.Play();
    }
  }
}
