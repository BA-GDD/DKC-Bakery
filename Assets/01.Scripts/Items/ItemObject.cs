using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemObject : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private ItemDataSO _itemData;

    private ItemObjectTrigger _trigger;
    private BoxCollider2D _itemCollider;
    private Transform _playerTrm;

    public bool asdf;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_itemData == null) return;
        if(_spriteRenderer == null )
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();//이건 워닝 
        }
        _spriteRenderer.sprite = _itemData.itemIcon;
        gameObject.name = $"ItemObject-[{_itemData.itemName}]";
    }
#endif

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>(); 
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _itemCollider = GetComponent<BoxCollider2D>();
        _trigger = transform.Find("ItemTrigger").GetComponent<ItemObjectTrigger>();
        _playerTrm = GameManager.Instance.PlayerTrm;
    }

    private void Start()
    {
        _trigger.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            asdf = true;
            _trigger.gameObject.SetActive(true);
            _itemCollider.enabled = false;
        }
        if(asdf)
            transform.position = Vector2.Lerp(transform.position, _playerTrm.position, 0.1f);
    }

    //이건 안쓰는 함수인데 혹시 몰라서 만들어둔다.
    public void SetUpItem(ItemDataSO itemData, Vector2 velocity)
    {
        _itemData = itemData;
        _rigidbody2d.velocity = velocity;
        _spriteRenderer.sprite = itemData.itemIcon;
    }

    public void PickUpItem()
    {
        Inventory.Instance.AddItem(_itemData);
        //여기에 인벤토리로 들어가는 코드 작성하고
        Destroy(gameObject);
    }
}
