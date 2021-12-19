using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BalloonsGravity : MonoBehaviour
{
    [SerializeField] private float force = 5f;
    [SerializeField] private float height = 2f;
    [SerializeField] private float heightOffset = 0.1f;
    [SerializeField] private Transform balloonGenerator;
    
    [HideInInspector] public float targetDistance;//Her bir balonun, balonların toplam merkezine olan uzaklığı 
    
    private bool _isCollide = false;

    private Vector3 _forceVector;

    private Rigidbody _body;

    private readonly Vector3 _forceOffset = 0.1f * Vector3.one;
    
    private float _angle;//Her bir balonun merkeze doğru gitmesi için gerekli açı.
    
    
    private Vector3 _forceOnStay;//Balondan ,balonların ortak merkezine doğru olan vektör.
    private Vector3 _targetPoint; //Balonların ortak merkezi.


    private void Start()
    {
        //İp uzunluğundaki hedefimizi belirliyoruz.
        _targetPoint = height * Vector3.up + balloonGenerator.position;
        
        _body = GetComponent<Rigidbody>();
    }

    
    private void Update()
    {
        _targetPoint = height * Vector3.up + balloonGenerator.position;

        _body.velocity = Vector3.zero;//Rigidbody'deki velocity'i 0'lıyoruz çünkü herhangi bir fizik etkileşimin olmasını istemiyoruz.

        targetDistance = (transform.position - _targetPoint).magnitude;//Hedef noktaya olan uzaklığımızı set ediyoruz.
        
        MoveBalloon();

        if (!_isCollide)//Herhangi bir balon objesiyle temas etmediğimiz kontrol ediyoruz.
        {
            MoveTarget();
        }
    }

   
    private void MoveBalloon() //Balonun ip uzunluğuna göre sabit kalmasını sağlayan method.
    {
        if ((transform.position-balloonGenerator.position).magnitude < height -heightOffset )//Eğer balonun ip uzunluğu olması gerekenden küçükse yukarı yönlü hareket ettiriyoruz.
        {
            transform.Translate(Vector3.up * force * Time.deltaTime);
            Debug.Log("Asagida");

        }
        else if ((transform.position-balloonGenerator.position).magnitude > height + heightOffset)//Eğer balonun ip uzunluğu olması gerekenden büyükse aşağı yönlü hareket ettiriyoruz.
        {
            transform.Translate(-Vector3.up * force * Time.deltaTime);
            Debug.Log("Yukarida");
        }
    }
    
    private void MoveTarget()//Balonların birlikte kalması üçün merkezlerine doğru kuvvet uyguluyoruz.
    {
        if(targetDistance > _forceOffset.magnitude)
        {
            var position = transform.position;
            
            _angle = Mathf.Acos(position.y / position.magnitude);//Balona uygulanan yukarı yönlü vektörle, balonun çıkış noktasıyla balon arasındaki vektörün arasındaki açıyı hesaplıyoruz.
            _forceOnStay = (_targetPoint-position);
            
            transform.Translate((_forceOnStay-_forceOffset)*Mathf.Sin(_angle) * Time.deltaTime);
            Debug.Log("dengede");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Balloon") && collision.gameObject.GetComponent<BalloonsGravity>().targetDistance < targetDistance)
            _isCollide = true;
        else
            _isCollide = false;
        
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Balloon"))
            _isCollide = false;
    }
}
