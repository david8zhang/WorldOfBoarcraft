using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = transform.parent.GetComponent<Enemy>();
        if (other.gameObject.layer == 8) {
            enemy.Stop();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Enemy enemy = transform.parent.GetComponent<Enemy>();
        if (other.gameObject.layer == 8) {
            enemy.Go();
        }
    }
}
