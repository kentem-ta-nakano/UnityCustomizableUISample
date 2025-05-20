using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetUIRetriever : MonoBehaviour
{
    [SerializeField]
    private GameObject _tablePanel;

    [SerializeField]
    private GameObject _cameraRenderPanel;

    [SerializeField]
    private Camera _UICamera;

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private EventSystem _eventSystem;

    private GraphicRaycaster _raycaster;

    void Start()
    {
        _raycaster = _canvas.GetComponent<GraphicRaycaster>();
    }

    public ControlableUIEnum? GetTargetUI(Vector2 screenPosition)
    {
        var hits = new List<RaycastResult>();

        var eventData = new PointerEventData(_eventSystem);
        eventData.position = screenPosition;
        _raycaster.Raycast(eventData, hits);

        foreach (var hit in hits)
        {
            if (hit.gameObject == _tablePanel)
            {
                return ControlableUIEnum.TablePanel;
            }
            else if (hit.gameObject == _cameraRenderPanel)
            {
                return ControlableUIEnum.CameraRenderPanel;
            }
        }

        return null;
    }
}
