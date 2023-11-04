using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Follow Settings")]
    [SerializeField] Collider2D CameraMoveArea;
    [SerializeField] Collider2D CameraViewArea;
    [SerializeField] float smoothSpeed = 0.5f;
    [SerializeField] float xOffset = 0f;
    [SerializeField] float yOffset = 3f;

    [SerializeField] bool canGoLeft = true;

    private GameObject player;
    private Transform followTransform;

    private float xMin, xMax, yMin, yMax;
    private float camViewXMin, camViewXMax;
    private float camX, camY;
    private Vector3 smoothPos;

    private float currentplayerX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        followTransform = player.transform;
        currentplayerX = player.transform.position.x;
        xMin = CameraMoveArea.bounds.min.x;
        xMax = CameraMoveArea.bounds.max.x;
        yMin = CameraMoveArea.bounds.min.y;
        yMax = CameraMoveArea.bounds.max.y;

    }

    private void Update()
    {
        camViewXMin = CameraViewArea.bounds.min.x;
        camViewXMax = CameraViewArea.bounds.max.x;
    }

    private void FixedUpdate()
    {
        if (!canGoLeft)
        {
            if (followTransform.position.x + xOffset > currentplayerX)
            {
                currentplayerX = followTransform.position.x + xOffset;
            }


            camX = Mathf.Clamp(followTransform.position.x + xOffset, currentplayerX, xMax);
            float limitedXPlayerPos = Mathf.Clamp(player.transform.position.x, camViewXMin, camViewXMax);
            player.transform.position = new Vector3(limitedXPlayerPos, player.transform.position.y, player.transform.position.z);
            camY = Mathf.Clamp(followTransform.position.y + yOffset, yMin, yMax);
            smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), smoothSpeed);
            this.transform.position = smoothPos;
        }
        else
        {
            camY = Mathf.Clamp(followTransform.position.y + yOffset, yMin, yMax);
            camX = Mathf.Clamp(followTransform.position.x + xOffset, xMin, xMax);
            smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), smoothSpeed);

            if (GameManager.Instance.player.transform.position.x < camViewXMin || GameManager.Instance.player.transform.position.x > camViewXMax)
            {
                float limitedXPlayerPos = Mathf.Clamp(player.transform.position.x, camViewXMin, camViewXMax);
                player.transform.position = new Vector3(limitedXPlayerPos, player.transform.position.y, player.transform.position.z);
            }
           

            this.transform.position = smoothPos;
        }


    }
}
