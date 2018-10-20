using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  public float speed = 3f;
  public int health = 5;
  Animator animator;
  AudioSource audioSource;
  Sword sword;
  public AudioClip swordClip;
  public AudioClip runClip;
  public AudioClip owClip;
  public AudioClip dieClip;

	// Use this for initialization
	void Start () {
    animator = gameObject.GetComponent<Animator>();
    sword = transform.Find("sword").GetComponent<Sword>();
    audioSource = gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
    if (health <= 0f) return;
    if (Input.GetKeyDown("q")) {
      playSound(swordClip);
      animator.Play("attack");
    } else if (!isAnimating()) {
      if (Input.GetKey("a")) {
        playSound(runClip, true);
        animator.Play("run");
        transform.localScale = new Vector3(-1, 1, 1);
        transform.Translate(Vector3.left * Time.deltaTime * speed);
      } else if (Input.GetKey("d")) {
        playSound(runClip, true);
        animator.Play("run");
        transform.localScale = new Vector3(1, 1, 1);
        transform.Translate(Vector3.right * Time.deltaTime * speed);
      } else {
        if (audioSource.clip == runClip) {
          audioSource.clip = null;
        }
        gameObject.GetComponent<Animator>().Play("idle");
      }
    }
	}

  bool isAnimating() {
    return (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("hurt")) &&
           (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1 ||
           animator.IsInTransition(0));
  }

  void onAttack() {
    sword.DoDamage();
  }

  void playSound(AudioClip clip, bool loop = false) {
    if (audioSource.clip != clip || !loop) {
      audioSource.clip = clip;
      audioSource.loop = loop;
      audioSource.Play();
    }
  }

  public void takeDamage() {
    health--;
    if (health <= 0) {
      playSound(dieClip);
      animator.Play("die");
    } else {
      playSound(owClip);
      animator.Play("hurt");
    }
  }
}
