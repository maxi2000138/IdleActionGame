using System;
using System.Collections;
using UnityEngine;

public class WheatSpawner : MonoBehaviour
{
    private GameObject _wheat;

    private void Start()
    {
        _wheat = Resources.Load("Wheat") as GameObject;
        StartCoroutine(Grow(0f));
    }

    public void SetGrowing(GameObject DeletedPart) => 
        StartCoroutine(Grow(10f, DeletedPart));

    IEnumerator Grow(float seconds, GameObject deleted = null)
    {
        yield return new WaitForSeconds(seconds);
        SpawnWheat();
        if(deleted != null)
            Destroy(deleted);
    }

    private void SpawnWheat() => 
        Instantiate(_wheat, transform.position, transform.rotation, transform);
}
