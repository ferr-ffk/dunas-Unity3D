using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class ControladorInvisivel : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _touchActionReference;

    [SerializeField]
    private RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var obj = typeof(RectTransform).GetProperty("boundingBox", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(rectTransform);

        Debug.Log(obj);
    }

}
