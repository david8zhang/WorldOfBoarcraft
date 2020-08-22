using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        Player player = transform.parent.GetComponent<Player>();
        int power = player.power;
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy && enemy.currentHealth > 0) {
            enemy.Damage(power);
        }
    }
}
