using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOrb : MonoBehaviour
{
    private GameObject orbSphere;
    private GameObject glowField;

    private ParticleSystem ps;

    private bool collected = false;

    void Start()
    {
        orbSphere = transform.GetChild(0).gameObject;
        glowField = transform.GetChild(1).gameObject;
        ps = transform.GetChild(2).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!collected && col.gameObject.CompareTag("Player"))
        {
            ps.Play();
            orbSphere.SetActive(false);
            glowField.SetActive(false);
            collected = true;
            OrbSettings orb = gameObject.GetComponent<Orb>().orbSettings;
            col.gameObject.GetComponent<Player>().CollectOrb(orb);
        }
        
    }

    private void Update()
    {
        if (collected && !ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
