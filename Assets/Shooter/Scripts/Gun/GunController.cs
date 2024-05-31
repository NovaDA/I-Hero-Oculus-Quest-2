using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Gun[] allGun;
    Gun equippedGun;

    private void Start(){
    }
    public void EquipGun(Gun gunToEquip){
        if(equippedGun != null) {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
    }

    public void EquipGun(int weaponIndex){
        EquipGun(allGun[weaponIndex]);
    }

    public void OnTriggerHold() /// Gun Controller to check whenever to shoot or not
    {
        if(equippedGun != null)
        {
            equippedGun.OnTriggerHold();
        }
    }

    public void OnTriggerRelease(){
        if (equippedGun != null)
        {
            equippedGun.OnTriggerRelease();
        }
    }

    public float GetHeight{
        get{
            return weaponHold.position.y;
        }
    }

    public void Aim(Vector3 aimPoint){
        if(equippedGun != null)
        {
            equippedGun.Aim(aimPoint);
        }
    }

    public void Reload(){
        if (equippedGun != null)
        {
            equippedGun.Reload();
        }
    }
}
