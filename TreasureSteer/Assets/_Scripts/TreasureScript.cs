using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameManager _gameManager;

    public bool Collected;
    private bool activeEnabled = false;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Collected) Collect();
        if (_gameManager.win) DisableCollider();
        if (_gameManager.lose) DisableCollider();
        if (_gameManager.pause)
        {
            activeEnabled = true;
            DisableCollider();
        }

        if (!_gameManager.pause && activeEnabled)
        {
            activeEnabled = false;
            EnableCollider();
        }
    }

    public void Collect()
    {
        this.gameObject.GetComponentInParent<PolygonCollider2D>().enabled = false;
        this.gameObject.transform.localScale = Vector3.one * 5;
        _animator.SetBool("Collected", true);
    }

    private void DisableCollider()
    {
        this.GetComponentInParent<PolygonCollider2D>().enabled = false;
    }

    private void EnableCollider()
    {
        this.GetComponentInParent<PolygonCollider2D>().enabled = true;
    }
}
