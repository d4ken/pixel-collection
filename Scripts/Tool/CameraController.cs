using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float baseWidth = 900f;
    [SerializeField] private float baseHeight = 1600f;

    private float currentAspect;
    private Camera cam;
    private Camera backgroundCamera;

    void Awake()
    {
        cam = GetComponent<Camera>();
        UpdateViewRect();
        
        // 背景黒塗り用カメラの追加
        var obj = new GameObject("BackgroundCamera");
        obj.transform.SetParent(cam.transform);
        backgroundCamera = obj.AddComponent<Camera>();
        // 何も映さない
        backgroundCamera.cullingMask = 0;
        // 黒で塗り潰し
        backgroundCamera.clearFlags = CameraClearFlags.SolidColor;
        backgroundCamera.backgroundColor = Color.black;
        // 優先度をメインカメラより上げる
        backgroundCamera.depth = cam.depth - 1;
        backgroundCamera.farClipPlane = 100;
    }

    void Update()
    {
        // 動作確認のためエディタ上では毎フレーム設定値を調整する
        if (Application.isEditor && Application.isPlaying)
        {
            UpdateViewRect();
        }
    }

    /// <summary>
    /// カメラの描画範囲をアスペクト比に応じて調整する
    /// </summary>
    private void UpdateViewRect()
    {
        // 現在のアスペクト比を取得し、変化がなければ更新しない
        float newAspect = (float)Screen.height / Screen.width;
        if (Mathf.Approximately(currentAspect, newAspect))
        {
            return;
        }

        currentAspect = newAspect;

        // 基準のアスペクト比
        float baseAspect = baseHeight / baseWidth;

        // カメラのViewportRectを設定しなおす
        if (baseAspect > currentAspect)
        {
            // 横が余る場合
            float height = 1f;
            float width = currentAspect / baseAspect;
            float posX = (1f - width) / 2f;
            float posY = 0f;
            cam.rect = new Rect(posX, posY, width, height);
        }
        else
        {
            // 縦が余る場合
            float height = baseAspect / currentAspect;
            float width = 1f;
            float posX = 0f;
            float posY = (1f - height) / 2f;
            cam.rect = new Rect(posX, posY, width, height);
        }
    }
}
