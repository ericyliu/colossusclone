using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

  HashSet<BossPart> bossParts = new HashSet<BossPart>();

	void OnTriggerEnter2D(Collider2D other) {
    BossPart bossPart = other.transform.GetComponent<BossPart>();
    if (bossPart != null) {
      bossParts.Add(bossPart);
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    BossPart bossPart = other.transform.GetComponent<BossPart>();
    if (bossPart != null) {
      bossParts.Remove(bossPart);
    }
  }

  public void DoDamage() {
    foreach (BossPart bossPart in bossParts) {
      bossPart.ReceiveDamage();
    }
  }

}
