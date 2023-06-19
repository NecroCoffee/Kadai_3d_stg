using UnityEngine;
using System.Collections;
public class CameraFollow : MonoBehaviour
{
    public Transform target; //カメラがついていくターゲットのポジション
    public float smoothing = 5f; //カメラのついていくスピード
    Vector3 offset; // カメラとターゲットの間の距離
    void Start()
    {
        // カメラとターゲットの間の距離を算出
        offset = transform.position - target.position;
    }
    // 物体が動く毎に呼び出される
    void FixedUpdate()
    {
        // カメラの移動先
        Vector3 targetCamPos = target.position + offset;
        // カメラを移動する
        transform.position = Vector3.Lerp(transform.position, targetCamPos,
       smoothing * Time.deltaTime);
    }
}

