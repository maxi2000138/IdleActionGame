using UnityEngine;

public class HarvestingSwitcher : MonoBehaviour
{
    [SerializeField] private SliceListener _sliceListener;

    public void EnableHarvesting() => 
        _sliceListener.CanHarvesting = true;

    public void DisableHarvesting() =>
        _sliceListener.CanHarvesting = false;
}
