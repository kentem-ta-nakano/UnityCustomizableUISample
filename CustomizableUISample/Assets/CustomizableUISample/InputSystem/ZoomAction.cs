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

    private float _preventActionValue;

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
        var position = Pointer.current.position.ReadValue();
        var uiObject = _uiRetriever.GetTargetUI(position);
        if(uiObject == null || uiObject == ControlableUIEnum.CameraRenderPanel)
            return;

        // ズームを行う
        var actionValue = context.ReadValue<float>();
        //フォントサイズとテーブルサイズのスケールを変更する（片方はコメントアウトしている）
        _tableScaleController.AdjustScale(_preventActionValue - actionValue);
        // _tableFontSizeController.AdjustSize(_preventActionValue - actionValue);

        _preventActionValue = actionValue;
    }
}
