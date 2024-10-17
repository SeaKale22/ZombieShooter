using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShoot : MonoBehaviour
{
    public Camera playerCam;
    private Transform _camDirection;
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;

    public float manaCost = 1;

    public PlayerController player;
    //public GameObject castFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Transform barrelLocation;
    
    [Tooltip("Specify time to destory the magic bullet")] [SerializeField] private float destroyTimer = 5f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;


    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (barrelLocation == null)
        {
            barrelLocation = transform;
        }
        _camDirection = playerCam.gameObject.transform;

    }

    //This function creates the bullet behavior
    public void Shoot()
    {
        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }
        if (player.ManaCheck(manaCost))
        {
            // Create a bullet and add force on it in direction the player cam is looking
            GameObject magicShot;
            magicShot = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation) as GameObject;
            magicShot.GetComponent<Rigidbody>().AddForce(_camDirection.forward * shotPower);
            Destroy(magicShot, destroyTimer);
            player.LooseMana(manaCost);
        }
    }
}
