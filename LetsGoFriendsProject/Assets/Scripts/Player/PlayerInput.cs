using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public PlayerMove playerMove;

    public UnityEvent onClickMouseLeft = new UnityEvent();

    private void Awake()
    {
        if (playerMove == null)
        {
            playerMove = GetComponent<PlayerMove>();
            if (playerMove == null)
            {
                Debug.LogWarning("PlayerMove 가 없습니다.");
            }
        }
    }

    private void Start()
    {
        RegisterEvents();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClickMouseLeft?.Invoke();
        }
    }

    private void RegisterEvents()
    {
        onClickMouseLeft.AddListener(playerMove.ChangeDirection);
    }
}
