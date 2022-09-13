using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool explode;
    private void Update()
    {
        if(explode) explosion();
    }
    public void explosion()
    {
        this.gameObject.GetComponentInParent<PolygonCollider2D>().enabled = false;
        this.gameObject.transform.localScale = Vector3.one * 10;
        _animator.SetBool("Triggered", true);
    }
}
