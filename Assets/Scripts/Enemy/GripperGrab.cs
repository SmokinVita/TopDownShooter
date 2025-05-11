using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GripperGrab : MonoBehaviour
{

    private Animator _anim;
    private PlayerMovement _player;
    private bool _isGrabbing = false;
    // Start is called before the first frame update
    void Start()
    {

        _anim = GetComponentInParent<Animator>();
        if (_anim == null)
        {
            Debug.Log("Gripper Animator is NULL!");
        }
    }

    public void IsGrabbing(bool grabbing)
    {
        _isGrabbing = grabbing;
        Debug.Log("Called grabbing true");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<PlayerMovement>();
            if (_player != null && _isGrabbing == true)
            {
                _player.PlayerGetsGrabbed(true);
                Debug.Log("GrabbingPlayer");
                StartCoroutine(PlayerHoldRoutine());
            }
        }
    }

    IEnumerator PlayerHoldRoutine()
    {
        yield return new WaitForSeconds(4);
        _player.PlayerGetsGrabbed(false);
        _anim.SetTrigger("LetGo");
        _isGrabbing = false;
    }
}
