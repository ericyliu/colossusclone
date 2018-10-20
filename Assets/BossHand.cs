using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour {
  enum State {
    PreAttacking,
    Attacking,
    PostAttacking1,
    PostAttacking2,
    Following,
  };

  public float speed = 1.5f;
  public float attackSpeed = 2f;
  GameObject player;
  Animator animator;
  AudioSource audioSource;
  State state = State.Following;
  public AudioClip boomClip;
	// Use this for initialization
	void Start () {
    animator = transform.GetComponent<Animator>();
		player = GameObject.Find("player");
    audioSource = transform.GetComponent<AudioSource>();
  }

	// Update is called once per frame
	void Update () {
    if (state == State.Attacking) {
      transform.Translate(Vector3.down * attackSpeed * Time.deltaTime);
      if (transform.position.y <= 0f) {
        state = State.PostAttacking1;
        playSound(boomClip);
        animator.Play("postattack");
      }
    }
    if (state == State.PostAttacking2) {
      transform.Translate(Vector3.up * speed * Time.deltaTime);
      if (transform.position.y >= 1f) {
        state = State.Following;
      }
    }
    if (state == State.Following) {
      if (Mathf.Abs(player.transform.position.x - transform.position.x) <= .1f) {
        state = State.PreAttacking;
        animator.Play("preattack");
      } else {
        Vector3 direction = new Vector3(player.transform.position.x - transform.position.x, 0, 0);
        transform.Translate(direction.normalized * speed * Time.deltaTime);
      }
    }
	}

  void Attack() {
    state = State.Attacking;
  }

  void LiftOff() {
    state = State.PostAttacking2;
  }

  void OnTriggerEnter2D(Collider2D other) {
    Player player = other.transform.GetComponent<Player>();
    if (player != null) {
      player.takeDamage();
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
