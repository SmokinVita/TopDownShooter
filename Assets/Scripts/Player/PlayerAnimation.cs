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
}
