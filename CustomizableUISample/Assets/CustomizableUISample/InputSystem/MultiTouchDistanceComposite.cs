using UnityEngine;
using System.ComponentModel;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

[DisplayName("MultiTouchDistance Composite")]
public class MultiTouchDistanceComposite : InputBindingComposite<float>
{
    // タッチ入力
    [InputControl(layout = "Touch")] public int touch0 = 0;
    [InputControl(layout = "Touch")] public int touch1 = 0;

    /// <summary>
    /// 初期化
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
#else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
    private static void Initialize()
    {
        // 初回にCompositeBindingを登録する必要がある
        InputSystem.RegisterBindingComposite(typeof(MultiTouchDistanceComposite), "MultiTouchDistanceComposite");
    }

    /// <summary>
    /// 値の大きさを返す
    /// </summary>
    public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
    {
        return ReadValue(ref context);
    }

    /// <summary>
    /// 二つのタッチ位置の距離を取得
    /// </summary>
    public override float ReadValue(ref InputBindingCompositeContext context)
    {
        var touchState0 = context.ReadValue<TouchState, TouchDeltaMagnitudeComparer>(touch0);
        var touchState1 = context.ReadValue<TouchState, TouchDeltaMagnitudeComparer>(touch1);

        //片方でもタッチされていなければ0を返す(Canceldフェーズに移行させる)
        if (!(touchState0.isInProgress && touchState1.isInProgress))
            return 0;

        // タッチ位置（スクリーン座標）
        var pos0 = touchState0.position;
        var pos1 = touchState1.position;

        //二つのタッチ位置の距離
        return (pos0 - pos1).magnitude;
    }
}
