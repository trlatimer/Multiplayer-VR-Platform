using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Weapon : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public int curAmmo;
    public int maxAmmo;
    public float bulletSpeed;
    public float shootRate;

    private float lastShootTime;

    public GameObject bulletPrefab;
    public Transform bulletSpawnLocation;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void TryShoot()
    {
        // Can we shoot?
        if (curAmmo <= 0 || Time.time - lastShootTime < shootRate) return;

        curAmmo--;
        lastShootTime = Time.time;

        // Update ammo UI
        //GameUI.instance.UpdateAmmoText();

        // Spawn bullet
        photonView.RPC("SpawnBullet", RpcTarget.All, bulletSpawnLocation.position, Camera.main.transform.forward);
    }

    [PunRPC]
    private void SpawnBullet(Vector3 pos, Vector3 dir)
    {
        // Spawn and orient
        GameObject bulletObj = Instantiate(bulletPrefab, pos, Quaternion.identity);
        bulletObj.transform.forward = dir;

        // Get bullet script
        Bullet bulletScript =  bulletObj.GetComponent<Bullet>();

        //Get the player
        PlayerController player = photonView.gameObject.GetComponent<PlayerController>();

        // Init and set velocity
        bulletScript.Initialize(damage, player.id, player.photonView.IsMine);
        bulletScript.rig.velocity = dir * bulletSpeed;
    }

    [PunRPC]
    public void GiveAmmo(int amount)
    {
        curAmmo = Mathf.Clamp(curAmmo + amount, 0, maxAmmo);

        // Update UI
        //=GameUI.instance.UpdateAmmoText();
    }
}
