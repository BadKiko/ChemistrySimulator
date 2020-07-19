using UnityEngine;
using UnityEngine.UI;

public class TipsOnObject : MonoBehaviour
{
    // Этот скрипт нужен чтобы показывать какие подсказки показывать на том или ином объекте

    [Header("Подсказки:")]
    [SerializeField] private string[] _tips;
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
        for (int i = 0; i < _tips.Length; i++) // Циклом просматриваем какие подсказки в массиве и создаем объекты в грид
        {
            var _instantieTip = Instantiate(_tipObject, _mainGrid.transform); // Создаем объект подсказки
            _instantieTip.transform.GetChild(_childInGrid).GetComponent<Text>().text = _tips[i]; // Изменяет текст на подсказку
            if (i == _tips.Length - 1)
            {
                _gridResizeScript.Resize();
            }
        }
    }
}
