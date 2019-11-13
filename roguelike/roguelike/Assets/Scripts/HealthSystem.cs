using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public List<Animator> hearts;
    GameManager gm;

    public void Init()
    {
        gm = GameManager.instance;
    }

    public void DamageEntity(HealthEntity damaged, HealthEntity attacker)
    {
        damaged.anim.SetTrigger("Damaged");
        damaged.health--;

        if (damaged == gm.player)
        {
            if (hearts.Count >= gm.player.health - 1)
            {
                hearts[damaged.health].SetTrigger("Damaged");
            }
        }
        if (attacker == gm.player)
        {
            damaged.health -= 2;
        }

        if (damaged.health <= 0)
        {
            StartCoroutine(Death(damaged));
        }
    }

    IEnumerator Death(HealthEntity he)
    {
        gm.entityList.healthEntities.Remove(he);
        if (he.npc)
            gm.entityList.npcEntities.Remove(he.npc);

        he.anim.SetBool("Death", true);
        he.gameObject.layer = 8;

        yield return null;
    }
}