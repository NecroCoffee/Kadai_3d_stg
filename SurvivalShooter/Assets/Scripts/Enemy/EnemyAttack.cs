using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{

    public float timeBetweenAttacks = 0.5f; // 攻撃する時間間隔
    public int attackDamage = 10; // 攻撃のダメージ量
    Animator anim; // アニメーション
    GameObject player; // プレイヤー
    PlayerHealth playerHealth; // PlayerHealth スクリプト
                               //EnemyHealth enemyHealth; 
    bool playerInRange; // 攻撃する範囲にプレイヤーが存在するかのフラグ
    float timer; // 経過時間
    void Awake()
    {
        // プレイヤーを取得
        player = GameObject.FindGameObjectWithTag("Player");
        // プレイヤーの HP を取得
        playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        // アニメータコンポーネントを取得
        anim = GetComponent<Animator>();
    }
    // 物体と衝突したときの処理

    void OnTriggerEnter(Collider other)
    {
        // 衝突した物体がプレイヤーの場合
        if (other.gameObject == player)
        {
            // 攻撃する範囲にプレイヤーが存在するかのフラグをオンにする
            playerInRange = true;
        }
    }
    // 衝突した物体が離れた時の処理
    void OnTriggerExit(Collider other)
    {
        // 衝突した物体がプレイヤーの場合
        if (other.gameObject == player)
        {
            // 攻撃する範囲にプレイヤーが存在するかのフラグをオフにする
            playerInRange = false;
        }
    }
    void Update()
    {
        // 経過時間を加算する
        timer += Time.deltaTime;
        // 攻撃の時間間隔よりも経過時間が長い、かつプレイヤーが攻撃範囲に存在する場合
        if (timer >= timeBetweenAttacks && playerInRange
       /* && enemyHealth.currentHealth > 0*/)
        {
            // プレイヤーを攻撃する
            Attack();
        }
        // プレイヤーの HP が 0 以下になった場合
        if (playerHealth.currentHealth <= 0)
        {
            // プレイヤーが倒された時のアニメーションフラグをオンにする
            anim.SetTrigger("PlayerDead");
        }
    }
    // 敵が攻撃する処理
    void Attack()
    {
        // 経過時間を 0 にする
        timer = 0f;
        // プレイヤーの HP が 0 よりも大きい場合
        if (playerHealth.currentHealth > 0)
        {
            // プレイヤーにダメージを与える
            playerHealth.TakeDamage(attackDamage);
        }

    }
}
