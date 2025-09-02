using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Vector3 _positionStrength;
    [SerializeField] private Vector3 _rotationStrength;

    private static event Action Shake;
    
    public static void Invoke()
    {
        Shake?.Invoke();
        print("Run");
    }

    private void OnEnable() => Shake += ShakeCamera;

    private void OnDisable() => Shake -= ShakeCamera;



    private void ShakeCamera()
    {
        _camera.DOComplete();
        _camera.DOShakePosition(0.3f, _positionStrength);
        _camera.DOShakeRotation(0.3f, _rotationStrength);
    }
}
