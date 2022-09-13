using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool Collected;

    private void Update()
    {
        if (Collected) Collect();
    }
    public void Collect()
    {
        this.gameObject.GetComponentInParent<PolygonCollider2D>().enabled = false;
        this.gameObject.transform.localScale = Vector3.one * 5;
        _animator.SetBool("Collected", true);
    }
}
