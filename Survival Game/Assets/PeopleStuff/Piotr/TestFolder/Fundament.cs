using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fundament : MonoBehaviour {

    public GameObject gotowy;
    public bool postawiony;
    public bool snapped;
    public LayerMask layer;
    Vector3 kierunek;
    GameObject org;
    float dystans = 3.5f;
    Vector3 pozycja;

    void Start ()
    {
        org = GameObject.FindGameObjectWithTag("Origin");
        kierunek = org.transform.position - Camera.main.transform.position;
    }
	
	void Update ()
    {
        
	}

    void LateUpdate()
    {
        pozycja = ustawPozycje();
        if (!postawiony && !snapped)
        {
            transform.position = pozycja;
            postaw();
        }
    }

    Vector3 ustawPozycje()
    {
        kierunek = org.transform.position - Camera.main.transform.position;
        kierunek.Normalize();
        kierunek *= dystans;
        Vector3 pozycja;

        RaycastHit hit;
        if (Physics.Raycast(org.transform.position, kierunek, out hit, dystans, layer))
        {
            pozycja = hit.point;
        }
        else
        {
            pozycja = org.transform.position + kierunek;
        }
        return pozycja;
    }

    void postaw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(gotowy, transform.position, Quaternion.identity);
        }
    }
}
