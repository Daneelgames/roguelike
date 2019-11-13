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

        foreach (HealthEntity he in gm.entityList.healthEntities)
        {
            he.healthMax = he.health;
        }
    }

    public void DamageEntity(HealthEntity damaged, HealthEntity attacker)
    {
        damaged.anim.SetTrigger("Damaged");
        damaged.health--;

        if (damaged == gm.player)
        {
            UpdateHearts();
        }

        if (damaged.health <= 0)
        {
            if (attacker == gm.player)
            {
                HealEntity(gm.player);
            }

            StartCoroutine(Death(damaged));
        }
    }

    IEnumerator SetActive(GameObject go, bool active, float time)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(active);
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

    void HealEntity(HealthEntity he)
    {
        if (he == gm.player)
        {
            he.health++;
            if (he.health > he.healthMax) he.health = he.healthMax;

            UpdateHearts();
        }
    }

    void UpdateHearts()
    {
        HealthEntity he = gm.player;

        for (int i = 1; i <= he.healthMax; i++)
        {
            if (he.health >= i)
            {
                print(i);
                StartCoroutine(SetActive(hearts[i - 1].gameObject, true, 0));
            }
            else
            {
                print(i);
                StartCoroutine(SetActive(hearts[i - 1].gameObject, false, 0));
            }
        }
    }
}