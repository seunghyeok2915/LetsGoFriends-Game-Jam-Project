using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public PlayerMove playerMove;
    public PlayerParry playerParry;

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

        if (playerParry == null)
        {
            playerParry = GetComponent<PlayerParry>();
            if (playerParry == null)
            {
                Debug.LogWarning("playerParry 가 없습니다.");
            }
        }
    }

    private void Start()
    {
        RegisterEvents();
    }

    private void Update()
    {
        if (!TitlePanel.isStart) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                onClickMouseLeft?.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerParry.OnSpaceBtn();
        }

 
    }

    private void RegisterEvents()
    {
        onClickMouseLeft.AddListener(playerMove.ChangeDirection);

    }
}
