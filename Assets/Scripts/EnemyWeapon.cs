using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if (player && !player.isSlash && !player.isBrandish) {
            player.ChangeHealth(-2);
        }
    }
}
