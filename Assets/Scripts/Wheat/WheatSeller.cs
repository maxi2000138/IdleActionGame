using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WheatSeller : MonoBehaviour
{
    private Coroutine _coroutine;
    
    [SerializeField] private Transform _sellPoint;
    [SerializeField] private Bank _bank;
    [SerializeField] private LoadSliderMover _loadSlider;
    [SerializeField] private Ease _moveEase = Ease.Linear;

    [SerializeField] private int _wheatCost = 15;

    [Header("Jump Settings")] [SerializeField]
    private float _jumpPower;
    [Range(0,1)]
    [SerializeField] private float _deltaBetweenObjects = 0.1f;
    
    [Range(0,10)]
    [SerializeField] private float _moveDuration;
    
    public void SellInventory(List<Transform> inventory, Action decreasePosition) => 
        _coroutine = StartCoroutine(SellGoods(inventory, decreasePosition));

    public void StopSellingInventory()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController)) 
            SellInventory(playerController.BagBehaviour.Wheats, playerController.BagBehaviour.DecreasePosition);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController)) 
            StopSellingInventory();
    }

    IEnumerator SellGoods(List<Transform> inventory, Action decreasePosition)
    {
        int inventoryCount = inventory.Count;
        for(int i = 0; i < inventoryCount; i++)
        {
            Transform item = inventory[inventory.Count-1];
            item.parent = null;
            DOTween.Sequence()
                .Append(item.DOJump(_sellPoint.position, _jumpPower, 1, _moveDuration))
                .AppendCallback(() => _bank.TryAddMoney(_wheatCost));
            inventory.Remove(item);
            _loadSlider.UpdateSlider();
            decreasePosition?.Invoke();
            yield return new WaitForSeconds(_deltaBetweenObjects);
        }
    }
}
