using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomAction : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset _actionAsset;

    [SerializeField]
    private TargetUIRetriever _uiRetriever;

    [SerializeField]
    private TableScaleController _tableScaleController;

    [SerializeField]
    private TableFontSizeController _tableFontSizeController;

    private InputAction _zoomAction;

    void Start()
    {
        var map = _actionAsset.FindActionMap("UI");
        map.Enable();

        _zoomAction = map.FindAction("Zoom");
        _zoomAction.performed += Zoom;
    }

    private void Zoom(InputAction.CallbackContext context)
    {
        // ズーム対象のUIを取得する
        var position = Mouse.current.position.ReadValue();
        var uiObject = _uiRetriever.GetTargetUI(position);
        if(uiObject == null)
            return;

        // ズームを行う
        var actionValue = context.ReadValue<Vector2>();
        Debug.Log(actionValue.y);
        _tableScaleController.AdjustScale(actionValue.y);
        // _tableFontSizeController.AdjustSize(actionValue.y);
    }
}
