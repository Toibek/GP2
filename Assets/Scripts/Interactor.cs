using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private Transform _interactionPoint;
    [SerializeField]
    private float _interactionPointRadius = 0.5f;
    [SerializeField]
    private LayerMask _interactibleMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactibleMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}

/*public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;
    public void Interact()
    {
        OnInteract?.Invoke();
    }
}*/
