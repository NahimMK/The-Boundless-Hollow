using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon: MonoBehaviour 
{
    private static int weaponID;
    public void WeaponChoose(int ID) {
        weaponID = ID;
        if (ID == 4)
        {
            weaponID = 6;
        }
        PlayerPrefs.SetInt("WeaponID", weaponID);
    }

    public int returnWeaponID() {
        PlayerPrefs.SetInt("WeaponID", weaponID);
        return weaponID;
        
    }
}
