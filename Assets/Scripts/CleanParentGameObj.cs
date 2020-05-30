using UnityEngine;

public class CleanParentGameObj : MonoBehaviour {
    private void Update() {
        if (transform.childCount == 0) {
            Destroy(gameObject);
        }
    }
}