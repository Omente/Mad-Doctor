using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.TAG_PLAYER_BULLET) || collision.gameObject.CompareTag(TagManager.TAG_ENEMY_BULLET))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
