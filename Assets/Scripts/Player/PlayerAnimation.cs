using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
            Debug.Log("Player animator is NULL!");
    }

    public void RunAnimation(float speed)
    {
        _anim.SetFloat("Speed", speed);
    }

    public void DeathAnimation()
    {
        _anim.SetTrigger("Death");
    }

    public void ShootAnim(int animIndex) //anim index 0 = basic shoot, 1 = circleShot, 3 = tripleShot
    {
        switch (animIndex)
        {
            case 1:
                _anim.SetTrigger("CircleShot");
                break;
            case 2:
                _anim.SetTrigger("TripleShot");
                break;
            default:
                _anim.SetTrigger("BasicShot");
                break;
        }
    }
}
