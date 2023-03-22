using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Stats : MonoBehaviour
{
    [SerializeField] private int _baseLevel = 25;
    [SerializeField] private int _baseExp;
    [SerializeField] private int _baseHealth = 380;
    [SerializeField] private int _basePhysicalAttack = 45;
    [SerializeField] private int _baseMagicalAttack = 165;
    [SerializeField] private int _baseAccuracy = 20;
    [SerializeField] private int _baseEvade = 15;
    [SerializeField] private int _basePhysicalDefense = 70;
    [SerializeField] private int _baseMagicalDefense = 100;

    private int _CurrentLevel;
    private int _CurrentExp;
    private int _CurrentHealth;
    private int _CurrentPhysicalAttack;
    private int _CurrentMagicalAttack;
    private int _CurrentAccuracy;
    private int _CurrentEvade;
    private int _CurrentPhysicalDefense;
    private int _CurrntMagicalDefense;

    public void start()
    {

        _CurrentLevel = _baseLevel;
        _CurrentExp = _baseExp;
        _CurrentHealth = _baseHealth;
        _CurrentPhysicalAttack = _basePhysicalAttack;
        _CurrentMagicalAttack = _baseMagicalAttack;
        _CurrentAccuracy = _baseAccuracy;
        _CurrentEvade = _baseEvade;
        _CurrentPhysicalDefense = _basePhysicalDefense;
        _CurrntMagicalDefense = _baseMagicalDefense;
    }
}
