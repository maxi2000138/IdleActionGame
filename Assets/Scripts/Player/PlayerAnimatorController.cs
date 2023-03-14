using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    
    private static readonly int _speedHash = Animator.StringToHash("Speed");
    private static readonly int _HarvestingHash = Animator.StringToHash("IsHarvesting");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float value) => 
        _animator.SetFloat(_speedHash, value);

    public void StartHarvesting() => 
        _animator.SetBool(_HarvestingHash, true);
    
    public void StopHarvesting() => 
        _animator.SetBool(_HarvestingHash, false);
    
}
