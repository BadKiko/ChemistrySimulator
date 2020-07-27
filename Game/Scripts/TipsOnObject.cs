
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TipsOnObject : MonoBehaviour
{

    // Этот скрипт нужен чтобы показывать какие подсказки показывать на том или ином объекте

    [Header("Подсказки:")]
    public List<string> _tips;
    [Header("Объект где показываются подсказки (референс):")]
    [SerializeField] private GameObject _tipObject;
    [Header("Родительский объект (грид):")]
    [SerializeField] private GameObject _mainGrid;
    [Space]
    [Header("Номер ребенка (текста) в гриде:")]
    [SerializeField] private int _childInGrid;
    [Header("GridResize Script:")]
    [SerializeField] private GridResize _gridResizeScript;


    public void TipsMassive()
    {

        _gridResizeScript.Clear();
        for (int i = 0; i < _tips.Count; i++) // Циклом просматриваем какие подсказки в массиве и создаем объекты в грид
        {
            var _instantieTip = Instantiate(_tipObject, _mainGrid.transform); // Создаем объект подсказки
            _instantieTip.transform.GetChild(_childInGrid).GetComponent<Text>().text = _tips[i]; // Изменяет текст на подсказку

            if (i == _tips.Count - 1)
            {
                _gridResizeScript.Resize();
            }
        }
    }

    [ContextMenu("AddTip")]
    public void AddTip(string TipText)
    {
        _tips.Add(TipText);
    }
    [ContextMenu("RemoveTip")]
    public void RemoveTip(int RemoveIndex)
    {
        _tips.RemoveAt(RemoveIndex);
    }
}

