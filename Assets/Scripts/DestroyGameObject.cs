using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        destroyGameObj(other, "Laser");
        destroyGameObj(other, "Enemy");
        destroyGameObj(other, "PowerUps");
    }
    private void destroyGameObj(Collider2D other, string gameObjectName) {
        Debug.Log(other.tag);
        if (other.tag == gameObjectName) {
            Destroy(other.gameObject);
        }
    }
}