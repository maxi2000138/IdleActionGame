using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagBehaviour : MonoBehaviour
{
    private Transform _freeSeat;
    private float _currentDelta = 0f;
    private const string _pickableTag = "Pickable";
    

    [SerializeField] private int _capacity = 40;
    [SerializeField]private LoadSliderMover _loadSlider;
    [Header("Temp Container Characteristics")]
    [SerializeField] private float _delta = 0.5f;


    public List<Transform> Wheats;
    public int Capacity => _capacity;
    public Transform FreeSeat
    {
        get
        {
            Transform tempTransform = new GameObject("tempContainer").transform;
            tempTransform.parent = transform;
            tempTransform.localRotation = Quaternion.identity;
            Vector3 newPos = transform.position;
            newPos.y += _currentDelta;
            tempTransform.position = newPos;
            return tempTransform;
        }
    }

    private void Awake()
    {
        Wheats = new List<Transform>();
    }

    private void Start()
    {
        _freeSeat = transform;
    }

    public bool TryToAddToBag(Transform wheat)
    {
        if (Wheats.Count < _capacity)
        {
            Wheats.Add(wheat);
            _currentDelta += _delta;
            return true;
        }
        
        return false;
    }
    
    
    public void DecreasePosition() => 
        _currentDelta -= _delta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_pickableTag) && Wheats.Count < _capacity)
            if (TryToAddToBag(other.transform))
            {
                other.gameObject.AddComponent<FlyToBag>().SetBagAndFly(transform, FreeSeat);
                _loadSlider.UpdateSlider();
            }
        

    }
}
