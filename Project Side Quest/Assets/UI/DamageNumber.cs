using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour {

    float damage;
    float lifeTimer;

    float xDirection;
    float yDirection;

    public void Init(float damageDealt)
    {
        lifeTimer = 0;
        damage = damageDealt;

        GetComponent<TextMesh>().text = damage.ToString();

        xDirection = Random.Range(-0.01f, 0.01f);
        yDirection = Random.Range(-0.01f, 0.01f);
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;

        transform.Translate(xDirection, yDirection, 0);

        if(lifeTimer >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
