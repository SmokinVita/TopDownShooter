using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOrb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = GetComponent<Player>();
            if (player != null)
            {
                player.Heal();
                Destroy(this.gameObject);
            }
        }
    }
}
