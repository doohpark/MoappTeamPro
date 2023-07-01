using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private AudioSource fireSound;
    // Start is called before the first frame update
    void Start()
    {
        fireSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayWeaponSound(){
        fireSound.Play();
    }
}
