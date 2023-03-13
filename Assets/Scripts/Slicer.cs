using System;
using UnityEngine;
using EzySlice;
using Random = UnityEngine.Random;

public class Slicer : MonoBehaviour
{
    private const string _pickableTag = "Pickable";

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _cuttingRisingForceScaler;
    [SerializeField] private float _cuttingRotationMaxAngle;
    [SerializeField] private Material materialAfterSlice;
    [SerializeField] private LayerMask sliceMask;
    [SerializeField] private GameObject _wheatBlock;
  
    public void Slice(Collider SlicedObject)
    {
        SlicedHull slicedObject = SliceObject(SlicedObject.gameObject, materialAfterSlice);
        GameObject lowerHullGameobject = slicedObject.CreateLowerHull(SlicedObject.gameObject, materialAfterSlice);
        lowerHullGameobject.transform.position = SlicedObject.transform.position;
        StartGrowing(SlicedObject.gameObject.transform.parent.gameObject, lowerHullGameobject);

        GameObject wheat = Instantiate(_wheatBlock, SlicedObject.transform.position, Quaternion.identity);
        wheat.tag = _pickableTag;
                
        MakeItPhysical(wheat);
        Destroy(SlicedObject.gameObject);
    }

    private void MakeItPhysical(GameObject obj)
    {
        Rigidbody component = obj.AddComponent<Rigidbody>();
        component.velocity = Vector3.up * _cuttingRisingForceScaler;
        component.rotation = Quaternion.Euler(RandomNum(),RandomNum(),RandomNum());
    }

    private void StartGrowing(GameObject lowWheat, GameObject deleted) => 
        lowWheat.GetComponent<WheatSpawner>().SetGrowing(deleted);
    
    private float RandomNum() => 
        Random.Range(-_cuttingRotationMaxAngle, _cuttingRotationMaxAngle);

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null) => 
        obj.Slice(obj.transform.position, Vector3.up, crossSectionMaterial);
}