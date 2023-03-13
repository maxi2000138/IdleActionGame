using System.Collections;
using UnityEngine;

public class FlyToBag : MonoBehaviour
{
    private Transform _bag;
    private Transform _bagTargetPoint;
    private Vector3 _startPosition;
    private float _flyTime = 1f;
    private float _height = 1f;
    private Quaternion _startRotation;

    IEnumerator Fly(float flyTime)
    {
        float delta, currentTime = 0f;
        while (currentTime < flyTime)
        {

            delta = currentTime / flyTime;
            Vector3 newPosition = Vector3.Lerp(_startPosition, _bagTargetPoint.position, delta);
            Quaternion newRotation = Quaternion.Slerp(_startRotation, _bagTargetPoint.rotation, delta);
            newPosition.y += Mathf.Sin(delta * Mathf.PI) * _height;
            transform.position = newPosition;
            transform.rotation = newRotation;
            currentTime += Time.deltaTime;
            yield return null;
        }

        transform.parent = _bag;
        transform.position = _bagTargetPoint.position;
        transform.rotation = _bagTargetPoint.rotation;
        Destroy(_bagTargetPoint.gameObject);
    }
    

    public void SetBagAndFly(Transform bag, Transform bagTargetPoint)
    {
        _bag = bag;
        _bagTargetPoint = bagTargetPoint;
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        
        if(TryGetComponent(out Rigidbody rigidbody)) 
            Destroy(rigidbody);
        
        if(TryGetComponent(out MeshCollider collider)) 
            Destroy(collider);
        
        StartCoroutine(Fly(_flyTime));
    }
}
