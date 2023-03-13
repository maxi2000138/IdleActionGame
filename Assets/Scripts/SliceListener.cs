using System;
using UnityEngine;

public class SliceListener : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Slicer slicer;
    public bool CanHarvesting = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (CanHarvesting)
        {
            slicer.Slice(other);
            CanHarvesting = false;
        }
    }
    
}
