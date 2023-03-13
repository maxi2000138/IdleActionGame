using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private PlayerAnimatorController _animatorController;
    private bool isHarvestring = false;
    private bool isMove = false;
    private Vector3 _centerBox;
    private Vector3 _sizeBox;

    [SerializeField] private SliceListener _sliceListener;
    [SerializeField] private BagBehaviour _bagBehaviour;
    [SerializeField] private GameObject _scythe;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Slicer _slicer;

    public BagBehaviour BagBehaviour => _bagBehaviour;
    public Vector3 CenterBox => _centerBox;
    public Vector3 SizeBox => _sizeBox;

    private void Awake()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        _centerBox = boxCollider.center;
        _sizeBox = boxCollider.size;
        _playerRigidbody = GetComponent<Rigidbody>();
        _animatorController = GetComponent<PlayerAnimatorController>();
    }

    private void FixedUpdate()
    {
        if (CantStartHarvest() && !isHarvestring)
            Move();
    }

    private bool CantStartHarvest()
    {
        Collider[] colliders =
            Physics.OverlapBox(transform.position + transform.TransformDirection(_centerBox), _sizeBox / 2, transform.rotation, _layerMask);

        
        if (DontHarvestButHeNeed(colliders))
            EnableHarvest(colliders);
        
        return (colliders.Length == 0);
    }

    private void SetRotationToTarget(Vector3 target)
    {
        transform.LookAt(target);
        Vector3 eulerAngles = transform.localEulerAngles;
        eulerAngles.x = 0;
        transform.localEulerAngles = eulerAngles;
    }

    private bool DontHarvestButHeNeed(Collider[] colliders)
    {
        return colliders.Length != 0 && !isHarvestring;
    }

    private void Move()
    {
        Vector2 inputVector =  new Vector2(_joystick.Horizontal, _joystick.Vertical);
        float inputVectorMagnitude = Mathf.Abs(inputVector.magnitude);
        _animatorController.SetSpeed(inputVectorMagnitude);
        Vector3 moveVector = new Vector3(inputVector.x, Physics.gravity.y * 5 * Time.fixedDeltaTime, inputVector.y);
        _playerRigidbody.velocity = moveVector * _playerSpeed;
        Vector3 rotationVector = new Vector3(inputVector.x, 0f, inputVector.y);
        if (rotationVector != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(rotationVector);
    }


    private void EnableHarvest(Collider[] colliders)
    {
        _animatorController.SetSpeed(0f);
        SetRotationToTarget(colliders[0].transform.position);
        _scythe.SetActive(true);
        _animatorController.StartHarvesting();
        isHarvestring = true;
    }
    

    public void StopHarvest()
    {
       _scythe.SetActive(false);
       _animatorController.StopHarvesting();
        isHarvestring = false;
    }
}

