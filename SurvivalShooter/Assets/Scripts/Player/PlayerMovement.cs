using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f; // プレイヤーの移動速度
    Vector3 movement; // プレイヤーの移動方向
    Animator anim; // アニメーションコンポーネント
    Rigidbody playerRigidbody; // Rigidbody コンポーネント
    int floorMask; // Floor と rayermask を使う
    float camRayLength = 100f; // カメラからの ray
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor"); // Floor の rayermask を作成
        anim = GetComponent<Animator>(); // Animator コンポーネントを取得
        playerRigidbody = GetComponent<Rigidbody>(); // Rigidbody コンポーネントを取得
    }
    void FixedUpdate()
    {
        // インプットから左右上下の移動量を-1 もしくは 1 で受け取る
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
    Move(h, v); // プレイヤーを動かす Move()を呼ぶ
        Turning(); // プレイヤーの方向を動かす Turning()を呼ぶ
        Animating(h, v); // プレイヤーのアニメーションを設定する Animating()を呼ぶ
    }
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v); //移動量を設定
                                // 移動するベクトルを 1 にし，移動する距離を設定する
        movement = movement.normalized * speed * Time.deltaTime;
        // プレイヤーのポジションを動かす
        playerRigidbody.MovePosition(transform.position + movement);
    }
    void Turning()
    {
        // カメラから，マウスで指している方向の ray を取得する
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.yellow);
        // ray が衝突した情報を取得する
        RaycastHit floorHit;
        // ray を飛ばして，床に衝突した場合の処理
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // マウスで指している場所と，プレイヤーの場所の差分を取得
            Vector3 playerToMouse = floorHit.point - transform.position;
            // プレイヤは y 座標には動かさない
            playerToMouse.y = 0f;
            // プレイヤーの場所から，マウスで指している場所への角度を取得
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            // プレイヤーの角度(プレイヤーの向き)を、新しく設定
            playerRigidbody.MoveRotation(newRotation);
        }
    }
    void Animating(float h, float v)
    {
        // プレイヤーの移動量が 0 以外の場合，walking を true にする
        bool walking = h != 0f || v != 0f;
        // アニメーションのパラメータ IsWalking を walking の値で設定する
        anim.SetBool("IsWalking", walking);
    }
}
