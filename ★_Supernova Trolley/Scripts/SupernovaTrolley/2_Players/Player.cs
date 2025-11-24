using SupernovaTrolley.Audios;
using UnityEngine;
using VContainer;

namespace SupernovaTrolley.Players
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    public class Player : MonoBehaviour
    {
        //システム
        [Inject] private readonly SEManager _seManager;

        //参照
        [SerializeField] private Transform _bulletSpawnPointR; //弾の発射位置
        [SerializeField] private Transform _bulletSpawnPointL; //弾の発射位置
        [SerializeField] private GameObject _bulletPrefab; //弾のプレハブ
        [SerializeField] private GameObject _bulletEffectPrefab; //弾のエフェクトプレハブ
        [SerializeField] private GameObject _assistR; //アシスト表示オブジェクト    
        [SerializeField] private GameObject _assistL; //アシスト表示オブジェクト

        //弾を発射できるかどうか
        private bool _canShoot = false;

        //弾の発射を開始・停止
        public void StartShooting()
        {
            _canShoot = true;
            _assistR.SetActive(true);
            _assistL.SetActive(true);

        }
        public void StopShooting()
        {
            _canShoot = false;
            _assistR.SetActive(false);
            _assistL.SetActive(false);
        }

        //連射回避用
        private bool _wasShootingR = false;
        private bool _wasShootingL = false;


        private void Update()
        {
            //弾を発射できる状態か確認
            if (!_canShoot) return;

            //右手からの発射
            if (InputManager.TriggerR_OnPress() > 0.5f)
            {
                //連射対策
                if (_wasShootingR) return;

                //弾を発射
                var bulletTr = _bulletSpawnPointR.rotation * Quaternion.Euler(90f, 0f, 0f);
                Instantiate(_bulletPrefab, _bulletSpawnPointR.position, bulletTr);
                Instantiate(_bulletEffectPrefab, _bulletSpawnPointR.position, _bulletSpawnPointR.rotation);
                _seManager.PlaySE(SE.Shoot);

                //連射対策
                _wasShootingR = true;
            }
            else
            {
                //連射対策
                _wasShootingR = false;
            }

            //左手からの発射
            if (InputManager.TriggerL_OnPress() > 0.5f)
            {
                //連射対策
                if (_wasShootingL) return;

                //弾を発射
                var bulletTr = _bulletSpawnPointL.rotation * Quaternion.Euler(90f, 0f, 0f);
                Instantiate(_bulletPrefab, _bulletSpawnPointL.position, bulletTr);
                Instantiate(_bulletEffectPrefab, _bulletSpawnPointL.position, _bulletSpawnPointL.rotation);
                _seManager.PlaySE(SE.Shoot);

                //連射対策
                _wasShootingL = true;
            }
            else
            {
                //連射対策
                _wasShootingL = false;
            }
        }
    }
}