using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnItems : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    bool isEmpty = true;
    int enter = 0;
    int exit = 0;
    void Start()
    {
        if (gameObject.name == "WeaponSpawner")
            objects = Resources.LoadAll<GameObject>("Weapons").ToList();
        else
            objects = Resources.LoadAll<GameObject>("Abilities").ToList();
        StartCoroutine(SpawnRandomObject());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enter++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        exit++;
        StartCoroutine(SpawnRandomObject());
    }

    IEnumerator SpawnRandomObject()
    {
        if (enter == exit) //ha bármi van a spawneren nem enged spawnolni semmit
            isEmpty = true;
        else
            isEmpty = false;
        if (isEmpty)
        {
            int whichObject = Random.Range(0, objects.Count);
            yield return new WaitForSeconds(10f);
            if (isEmpty)
            {
                isEmpty = false;
                Instantiate(objects[whichObject], new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            }
        }
    }
}
