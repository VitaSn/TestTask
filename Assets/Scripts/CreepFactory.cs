using System.Collections.Generic;
using UnityEngine;

public class CreepFactory : MonoBehaviour
{
    [SerializeField] List<GameObject> Prefabs = new List<GameObject>();
    private static List<GameObject> _prefabs = new List<GameObject>();
    private static Transform _enable;
    private static Transform _disable;
    private static List<GameObject> _enables = new List<GameObject>();
    private static List<GameObject> _disables = new List<GameObject>();

    private void Start()
    {
        _prefabs = Prefabs;
        
        _enable = new GameObject().transform;
        _enable.parent = transform;
        _enable.gameObject.name = "Enable";

        _disable = new GameObject().transform;
        _disable.parent = transform;
        _disable.gameObject.name = "Disable";
    }
    
    public static T Instantiate<T> (Vector3 position, Transform parent = null)
    {
        GameObject obj = Find<T>(_disables);
        if (obj == null)
        {
            obj = Instantiate(Find<T>(_prefabs), _disable);

            if (obj != null)
                _enables.Add(obj);
            else
                return default(T);
        }
        else
        {
            _disables.Remove(obj);
            _enables.Add(obj);
        }

        if (parent == null)
            obj.transform.parent = _enable;
        else
            obj.transform.parent = parent;

        obj.transform.position = position;    
        obj.SetActive(true);
        obj.TryGetComponent<T>(out T prefab);
        return prefab;
    }

    public static void Destroy (GameObject prefab)
    {
        if (prefab == null) return;
        
        _enables.Remove(prefab);
        _disables.Add(prefab);
        prefab.transform.parent = _disable;
        prefab.SetActive(false);
    }

    private static GameObject Find<T> (List<GameObject> list)
    {
        foreach (var prefab in list)
        {
            if (prefab.TryGetComponent<T>(out T obj)) return prefab;
        }
        return null;
    }

    public void OnDestroy ()
    {
        foreach (var prefab in _enables)
        {
            if (prefab)
            {
                _disables.Remove (prefab);
                Destroy (prefab);
            }
        }
        foreach (var prefab in _disables)
        {
            if (prefab)
            {
                _disables.Remove (prefab);
                Destroy (prefab);
            }
        }
    }
}
