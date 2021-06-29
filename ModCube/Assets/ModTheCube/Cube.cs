using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    private Vector3 newPosition;
    private Vector3 newScale;
    private Color newColor;
    private float rotationSpeed;
    private float speed = 1.0f;
    private float startDelay = 1.0f;
    private float chagneInterval = 2.0f;
    private float locationRandom = 7.0f;
    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;
        
        Material material = Renderer.material;

        InvokeRepeating("TargetLotation", startDelay, chagneInterval);
        InvokeRepeating("ChangeScale", startDelay, chagneInterval);
        InvokeRepeating("ChangeRotationSpeed", startDelay, chagneInterval);
        InvokeRepeating("ChangeColor", startDelay, chagneInterval);
    }
    
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, 0.0f);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
        transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * speed);
        Renderer.material.color = Color.Lerp(Renderer.material.color, newColor, Time.deltaTime * speed * 5);
        
    }

    // change location
    void TargetLotation()
    {
        float x, y, z;
        x = Random.Range(-locationRandom, locationRandom);
        y = Random.Range(-locationRandom, locationRandom);
        z = Random.Range(-locationRandom, locationRandom);
        newPosition = new Vector3(x, y, z);
    }

    // change scale
    void ChangeScale()
    {
        float x, y, z;
        x = Random.Range(1.0f, 5.0f);
        y = Random.Range(1.0f, 5.0f);
        z = Random.Range(1.0f, 5.0f);
        newScale = new Vector3(x, y, z);
    }

    // change rotation speed
    void ChangeRotationSpeed()
    {
        rotationSpeed = Random.Range(1.0f, 200.0f);
    }

    // change material color
    void ChangeColor()
    {
        newColor = new Color(RandomColor(), RandomColor(), RandomColor());
    }
    float RandomColor()
    {
        float randomColor = Random.Range(0.0f, 1.0f);
        return randomColor;
    }
    // change material opacity
}
