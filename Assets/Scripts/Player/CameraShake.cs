using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _camera;
    [SerializeField] private float _shakeDuration = .5f;

    private void Start()
    {
        _camera = FindAnyObjectByType<CinemachineImpulseSource>();
        if (_camera == null)
            Debug.Log("CinemachineImpluseSource is NULL!");
    }

    public void CallForShake()
    {
        //StartCoroutine(ShakeCameraRoutine());
        _camera.GenerateImpulse();
        Debug.Log("Called for the Shake");
    }

    IEnumerator ShakeCameraRoutine()
    {
        Vector3 cameraOrgPos = _camera.transform.localPosition;

        while (_shakeDuration > .01f)
        {
            var x = Random.Range(-1f, 1f);
            var y = Random.Range(-1f, 1f);

            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(cameraOrgPos.x + x, cameraOrgPos.y + y, cameraOrgPos.z), .5f);

            _shakeDuration -= Time.deltaTime;

            yield return new WaitForSeconds(.1f);
        }

        transform.localPosition = cameraOrgPos;
    }
    //call method from player
    //start coroutine to start camera shake
    //grab camera orginal pos
    //randomly shake camera
    //reset camera to orginal pos

}
