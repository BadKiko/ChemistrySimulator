using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemSystem : MonoBehaviour
{
    [Header("Таблица Менделеева:")]
    [SerializeField] private string[] ChemElements;

    [SerializeField] private int[] ElementsChelochnMetalls;
    [SerializeField] private int[] ChelochnZemelnMetalls;
    [SerializeField] private int[] PerehodnMetalls;

    [SerializeField] private int[] NotMetalls;
    [SerializeField] private int[] HalfMetalls;
    [SerializeField] private int[] Halkogens;
    [SerializeField] private int[] Galogens;
    [SerializeField] private int[] InertnieGas;

    [SerializeField] private int[] FluidElements;
    [SerializeField] private int[] SandElements;
    [SerializeField] private int[] GasElements;


    private void Start()
    {
        for (int i = 0; i <= ElementsChelochnMetalls.Length; i++) // Поиск щелочных металлов в таблице
        {
            Debug.Log("Cheloch + " + ChemElements[ElementsChelochnMetalls[i]]);
        }
    }
}
