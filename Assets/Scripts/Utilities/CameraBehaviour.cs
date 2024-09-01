using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform _playerTransform;

    [Header("Variables of control")]
    [SerializeField] private bool isFollow;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isFollow = true;
    }

    void FixedUpdate()
    {
        if (!isFollow) return;

        var desiredPosition = _playerTransform.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
