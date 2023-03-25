using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformas : MonoBehaviour
{
    private Transform player;
    public GameObject[] plataformasmalas;
    float force= 500f;
    float radius= 100f;
    public float velocidadMinimaParaDestruir = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("ball").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        
        if(transform.position.y > player.position.y)
        {
            for(int i = 0; i < plataformasmalas.Length; i++)
            {
                Rigidbody rb = plataformasmalas[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;

                    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

                    foreach (Collider newCollider in colliders)
                    {
                        Rigidbody otherRB = newCollider.GetComponent<Rigidbody>();
                        if (otherRB != null && otherRB.velocity.magnitude >= velocidadMinimaParaDestruir)
                        {
                            otherRB.AddExplosionForce(force, transform.position, radius);
                        }
                    }
                }

                plataformasmalas[i].transform.parent = null;
                Destroy(plataformasmalas[i].gameObject, 2f);
                Destroy(this.gameObject, 5f);
            }
            this.enabled = false;
        }
    }
}
