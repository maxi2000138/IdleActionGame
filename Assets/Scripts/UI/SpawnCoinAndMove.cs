using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCoinAndMove : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;
    [Header("Coin animation settings")]
    [SerializeField] private Ease _flyEase;
    [SerializeField] private Ease _scaleEase;
    [Range(0,6)]
    [SerializeField] private float _flyDuration;
    [Range(0,6)]
    [SerializeField] private float _growDuration;
    [SerializeField] private float _xOffset = 1f;
    [SerializeField] private float _yOffset = 1f;
    private Vector3 _targetLocalScale;

    private void Start()
    {
        _targetLocalScale = _target.localScale;
    }

    public void InstantiateCoinAndMoveToPoint(Action addMoney)
    {
        float xOffset = Random.Range(-_xOffset, _xOffset);   
        float yOffset = Random.Range(-_yOffset, _yOffset);
        Vector3 position = transform.position;
        position.x += xOffset;
        position.y += yOffset;
        GameObject coin = Instantiate(_prefab, position, Quaternion.identity, transform);
        Vector3 localScale = coin.transform.localScale;
        coin.transform.localScale = Vector3.zero;
        Vector3 punchVector = _target.position - coin.transform.position;
        DOTween.Sequence()
            .Append(coin.transform.DOScale(localScale, _growDuration).SetEase(_scaleEase))
            .Append(coin.transform.DOMove(_target.position, _flyDuration).SetEase(_flyEase))
            .AppendCallback(() => addMoney?.Invoke())
            .AppendCallback(returner)
            .Append(_target.transform.DOPunchScale(_targetLocalScale*0.15f, 0.25f, 1, 1f))
            .AppendCallback(() => Destroy(coin));

    }

    public void returner() => 
        _target.localScale = _targetLocalScale;
}
