using UnityEngine;

public class TableScaleController : MonoBehaviour
{
    private RectTransform _rectTransform;

    private float _minScale = 0.5f;
    private float _maxScale = 1.5f;

    private float _deltaLimit = 0.01f;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

    }

    public void ResetScale(){
        _rectTransform.localScale = Vector3.one;
    }

    public void AdjustScale(float delta){
        if(delta > _deltaLimit)
            delta = _deltaLimit;
        else if(delta < -1 * _deltaLimit)
            delta = -1 * _deltaLimit;

        _rectTransform.localScale += new Vector3(delta, delta, delta);

        if(_rectTransform.localScale.x > _maxScale){
            _rectTransform.localScale = new Vector3(_maxScale, _maxScale, _maxScale);
        }else if(_rectTransform.localScale.x < _minScale){
            _rectTransform.localScale = new Vector3(_minScale, _minScale, _minScale);
        }
    }
}
