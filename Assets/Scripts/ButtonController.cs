using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    GameManager gameManager;

    CapsuleCollider2D capsuleCollider; // To make sure there will be only Horizontal/Vertical Collisions
    RectTransform rectTransform;
    List<Collider2D> colliders = new List<Collider2D> ();
    ContactFilter2D contactFilter = new ContactFilter2D ();

    const int neighborNeeded = 2; // Number of Neigbors(Horizontal or Vertical) needed

    void Start () {
        capsuleCollider = GetComponent<CapsuleCollider2D> ();
        Canvas.ForceUpdateCanvases ();
        rectTransform = GetComponent<RectTransform> ();
        capsuleCollider.size = new Vector2 (rectTransform.rect.width, rectTransform.rect.height);

        gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (capsuleCollider.OverlapCollider (contactFilter, colliders) >= neighborNeeded) {
            foreach (Collider2D collider in colliders) {
                ButtonUpdate (collider.gameObject, false);
            }
            ButtonUpdate (gameObject, false);
            gameManager.NewMatch ();
        }
    }

    public void OnClick () {
        ButtonUpdate (gameObject, true);
    }

    void ButtonUpdate (GameObject button, bool isEnabled) {
        button.GetComponent<CapsuleCollider2D> ().enabled = isEnabled;
        button.transform.GetChild (0).gameObject.SetActive (isEnabled);
    }
}