using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static T Bind<T>(GameObject parent, string path) where T : Component
    {
        Transform transform = parent.transform.Find(path);
        if (transform == null)
        {
            Debug.LogWarning(path + "Not Found");
        }

        T component = transform.GetComponent<T>();
        if (component == null)
        {
            Debug.LogWarning(typeof(T).ToString() + "Not Found");
        }

        return component;
    }
}
