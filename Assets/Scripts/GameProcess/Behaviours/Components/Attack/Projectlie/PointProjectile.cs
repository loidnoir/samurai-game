using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointProjectile : Projectile
{
    public RandomFloat random;
    private Color Color { get=>_color; set { } }

    [SerializeField] private Color _color;

    [SerializeField] private Material pointMaterial;
    [SerializeField] private Material trailMaterial;

    private new Collider collider;
    private Vector3 startScale;

    private Coroutine coroutine;
    private Coroutine scaleCoroutine;

    protected override void Initialize()
    {
        collider = GetComponent<Collider>();
        startScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    protected override void Appear()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
            yield return scaleCoroutine = StartCoroutine(ScaleEnumerator(startScale));
            collider.enabled = true;
        }
    }
    protected override void Hide()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
            yield return scaleCoroutine = StartCoroutine(ScaleEnumerator(Vector3.zero));
            collider.enabled = false;
        }
    }
    protected override void OnHitting(Damagable damagable)
    {
        damagable.ChangeHealth(-damage);
        transform.localScale = Vector3.zero;
        collider.enabled = false;
    }

    IEnumerator ScaleEnumerator(Vector3 target)
    {
        while (Vector3.Distance(transform.localScale, target) > 0.01f)
        {
            transform.localScale = Vector3.LerpUnclamped(transform.localScale, target, Time.deltaTime * 10);
            yield return null;
        }

        transform.localScale = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Damagable damagable))
            OnHitting(damagable);
    }
}
