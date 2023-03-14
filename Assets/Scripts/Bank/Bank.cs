using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    
    private int _money = 0;
    
    [SerializeField] private SpawnCoinAndMove _coinSpawner;
    [SerializeField] private TMP_Text _moneyTxt;
    
    [Range(0f,1f)]
    [SerializeField] private float _moneyAppearingDeltaTime;

    private int _moneyNeededToShow = 0;
    private Coroutine _moneyCoroutine;

    private void Start() =>
        UpdateCointText();

    public void TryAddMoney(int money)
    {
        if (money < 0)
            throw new ArgumentOutOfRangeException();


        _coinSpawner.InstantiateCoinAndMoveToPoint(() =>
        {
            _moneyNeededToShow += money;
            StartCoinTextUpdatingCoroutine();
        });
    }

    private void StartCoinTextUpdatingCoroutine()
    {
        if(_moneyCoroutine == null)
            _moneyCoroutine = StartCoroutine(ChangeMoneyInText());
    }

    public IEnumerator ChangeMoneyInText()
    {
        while (_moneyNeededToShow > 0)
        {
            AddMoneyToBank(1);
            UpdateCointText();
            _moneyNeededToShow--;
            yield return new WaitForSeconds(_moneyAppearingDeltaTime);
        }

        _moneyCoroutine = null;
    }

    private void UpdateCointText() => 
        _moneyTxt.text = _money.ToString();

    private void AddMoneyToBank(int money) => 
        _money += money;
}
