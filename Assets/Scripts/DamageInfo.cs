using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo : MonoBehaviour
{

    [SerializeField] private GameObject Attack1,Attack2,Attack3;


void Start()
{

}

public void EnableAttack1()
{
        Attack1.SetActive(true);
}

public void EnableAttack2()
{
        Attack2.SetActive(true);
}


public void EnableAttack3()
{
        Attack3.SetActive(true);
}


public void DisableAttack1()
{
        Attack1.SetActive(false);
}

public void DisableAttack2()
{
        Attack2.SetActive(false);
}


public void DisableAttack3()
{
        Attack3.SetActive(false);
}



    public void EnableWeapon()
    {


    }//
    public void DisableWeapon()
    {
        //Attack1.SetActive(false);
    }


}
