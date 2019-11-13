using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    Vector3 currentPosition;
    [HideInInspector]
    public Animator anim;
    GameManager gm;

    public LayerMask movementMask;

    public void Init()
    {
        gm = GameManager.instance;
        currentPosition = gm.player.transform.position;
        anim = gm.player.GetComponent<Animator>();
    }

    public IEnumerator Move(Vector3 add, bool findClosestPoint)
    {
        Vector3 newPos = currentPosition + add;
        GameObject targetObject = CanMove(newPos);

        if (targetObject == null)
        {
            // Can not move
        }
        else if (targetObject.layer == 8) // tile
        {
            gm.CancelInvoke();
            gm.playerInput.movementCooldown = gm.playerInput.movementCooldownMax;

            gm.player.transform.position = currentPosition;
            gm.player.transform.LookAt(newPos);
            print(targetObject.name);
            anim.SetTrigger("Move");

            for (int t = 0; t < 3; t++)
            {
                gm.player.transform.position = Vector3.Lerp(gm.player.transform.position, newPos, 0.5f);
                yield return new WaitForSeconds(0.01f);
            }
            gm.player.transform.position = newPos;
            currentPosition = gm.player.transform.position;

            gm.attackSystem.PlayerMoved(targetObject);

            gm.Step(GameManager.GameEvent.PlayerAct);
        }
        else if (targetObject.layer == 9) // wall
        {
            gm.CancelInvoke();
            // Move into wall
        }
        else if (CanMove(newPos).layer == 10) // move into other unit
        {
            gm.CancelInvoke();
            gm.attackSystem.PlayerMoved(targetObject);
            gm.Step(GameManager.GameEvent.PlayerAct);
        }
    }

    GameObject CanMove(Vector3 targetPos)
    {
        RaycastHit hit;
        Physics.Raycast(targetPos + Vector3.up * 5f, Vector3.down, out hit, 10, movementMask);
        return hit.collider.gameObject;   
    }

    public IEnumerator NpcMove()
    {
        foreach (NpcEntity npc in gm.entityList.npcEntities)
        {
            if (npc.moveCooldown > 0)
                npc.moveCooldown--;
            else
            {
                Vector3 newPos = npc.transform.position + RandomDirection();

                if (CanMove(newPos).layer == 8)
                {
                    SavePosition(npc);
                    npc.transform.LookAt(newPos);
                    npc.health.anim.SetTrigger("Move");
                    StartCoroutine(NpcMoveSmooth(npc, newPos));
                }
            }
        }

        yield return new WaitForSeconds(0.1f);

        gm.Step(GameManager.GameEvent.NpcMove);
    }

    IEnumerator NpcMoveSmooth(NpcEntity npc, Vector3 newPos)
    {
        for (int t = 0; t < 9; t++)
        {
            npc.transform.position = Vector3.Lerp(npc.transform.position, newPos, 0.5f);
            yield return new WaitForSeconds(0.01f);
        }
        npc.transform.position = newPos;

        SavePosition(npc);
    }

    void SavePosition(NpcEntity npc)
    {
        npc.savedPosition = npc.transform.position;
        npc.savedPosition = new Vector3(Mathf.Round(npc.savedPosition.x), Mathf.Round(npc.savedPosition.y), Mathf.Round(npc.savedPosition.z));
        npc.transform.position = npc.savedPosition;
    }

    Vector3 RandomDirection()
    {
        int r = Random.Range(0, 4);

        switch (r)
        {
            default:
                return new Vector3(0, 0, 1);

            case 0:
                return new Vector3(0, 0, 1);
            case 1:
                return new Vector3(1, 0, 0);
            case 2:
                return new Vector3(0, 0, -1);
            case 3:
                return new Vector3(-1, 0, 0);
        }
    }
}