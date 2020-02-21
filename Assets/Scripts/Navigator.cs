using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public GameObject orbArrowPrefab;
    public GameObject asteroidArrowPrefab;

    //public GameObject planetArrowPrefab;
    //public GameObject starArrowPrefab;
    public GameObject blackHoleArrowPrefab;

    public LevelManager levelManager;


    private Dictionary<GameObject, GameObject> objects;
    private Dictionary<string, GameObject> icons;

    private float height;
    private float width;

    private float widthStart;
    private float widthEnd;
    
    private float heightStart;
    private float heightEnd;




    private void UpdateIcon(GameObject obj, GameObject pointerIcon)
    {
        Vector3 objPos = obj.transform.position;
        Vector3 dist = objPos - transform.position;

        pointerIcon.transform.position = transform.position + dist.normalized;
        pointerIcon.transform.up = dist.normalized;
    }

    private void AddObject(GameObject obj)
    {
        if (!objects.ContainsKey(obj) && icons.ContainsKey(obj.tag))
        {
            objects[obj] = Instantiate(icons[obj.tag]);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("BlackHole")) AddObject(collision.gameObject);
    }

    private void RemoveObject(GameObject obj)
    {
        if (objects.ContainsKey(obj))
        {
            Destroy(objects[obj]);
            objects.Remove(obj);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveObject(collision.gameObject);
    }

    private bool OnCamera(GameObject obj)
    {
        bool onCam = false;
        int count = 0;
        if (widthStart <= obj.transform.position.x && obj.transform.position.x <= widthEnd)
            count += 1;

        if (heightStart <= obj.transform.position.y && obj.transform.position.y <= heightEnd)
            count += 1;

        if (count == 2 && icons.ContainsKey(obj.tag))
            onCam = true;

        return onCam;
    }

    private void Start()
    {
        objects = new Dictionary<GameObject, GameObject>();
        icons = new Dictionary<string, GameObject>
        {
            ["Orb"] = orbArrowPrefab,
            ["Asteroid"] = asteroidArrowPrefab,
            ["BlackHole"] = blackHoleArrowPrefab
    };


        Camera cam = transform.GetChild(0).GetComponent<Camera>();
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;

        width = 2.0f * camHalfWidth;
        height = 2.0f * camHalfHeight;
        
    }


    private void LateUpdate()
    {
        widthStart = transform.position.x - (width / 2);
        widthEnd = transform.position.x + (width / 2);

        heightStart = transform.position.y - (height / 2);
        heightEnd = transform.position.y + (height / 2);

        //"for each" loops is not appropriate for deleting items from our dictionary while we were iterating

        List<GameObject> keyList = new List<GameObject>(objects.Keys);

        for(int i = 0; i<keyList.Count; i++)
        {
            if (OnCamera(keyList[i]))
                RemoveObject(keyList[i]);
            else UpdateIcon(keyList[i], objects[keyList[i]]);
        }

        if (levelManager.IsLevelCompleted())
        {
            AddObject(GameObject.FindGameObjectWithTag("BlackHole").gameObject);
        }



    }


}
