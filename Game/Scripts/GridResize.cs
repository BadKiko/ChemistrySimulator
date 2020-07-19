using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridResize : MonoBehaviour
{
    // Этот скрипт будет ресайзить грид, потому что юнити очень по уебски распределяет подсказки

    [Header("На сколько прибавлять высоту")]
    [SerializeField] private float _resizeScale;
    [Header("Стандартный Height у Grid")]
    [SerializeField] private float _standartSizeY;
    [Header("Стандартный PosY у Grid")]
    [SerializeField] private float _standartPosY;

    public void Resize()
    {
        var _gridTransform = this.gameObject.GetComponent<RectTransform>(); // Получаем рект трансформ у грида

        _gridTransform.sizeDelta = new Vector2(_gridTransform.sizeDelta.x, _standartSizeY); // Задаем стандартный размер
        _gridTransform.localPosition = new Vector2(_gridTransform.localPosition.x, _standartPosY); // Перетаскиваем на стандартную точку

        int _childCount = this.gameObject.transform.childCount;

        Debug.Log(_childCount);

        for(int i = 0; i < _childCount; i++) // если i меньше или равно количеству детей в гриде то мы останавливаем цикл
        {
            _gridTransform.sizeDelta = new Vector2(_gridTransform.sizeDelta.x, _gridTransform.sizeDelta.y + _resizeScale); // Задаем новую высоту у грида
            _gridTransform.position = new Vector2(_gridTransform.position.x, _gridTransform.position.y + _resizeScale / 2); // Перетаскиваем вверх и
        }
    }
}
