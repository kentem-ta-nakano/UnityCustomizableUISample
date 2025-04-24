using TMPro;
using UnityEngine;

public class TableFontSizeController : MonoBehaviour
{
    private TextMeshProUGUI[] _textList;

    private float _currentSize = 24f;

    private float _defaultSize = 24f;
    private float _minSize = 8f;
    private float _maxSize = 64f;

    private float _deltaLimit = 0.5f;

    void Start()
    {
        ReloadTexts();
        ResetSize();
    }

    public void ResetSize(){
        SetFontSize(_defaultSize);
    }

    public void AdjustSize(float delta){
        if(delta > _deltaLimit)
            delta = _deltaLimit;
        else if(delta < -1 * _deltaLimit)
            delta = -1 * _deltaLimit;
        
        var newSize = _currentSize + delta;

        if(newSize > _maxSize){
            newSize = _maxSize;
        }else if(newSize < _minSize){
            newSize = _minSize;
        }

        SetFontSize(newSize);
    }

    public void ReloadTexts(){
        _textList = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void SetFontSize(float newSize){
        _currentSize = newSize;
        foreach(var text in _textList){
            text.fontSize = newSize;
        }
    }
}
