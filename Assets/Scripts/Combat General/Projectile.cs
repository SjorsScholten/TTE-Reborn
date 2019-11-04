using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector3 direction;
    private float speed;
    private float duration;

    private bool attack = false;

    private ObjectPooler pooler;

    private void Awake() {
        pooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update() {
        if (attack) {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void Launch(Vector3 direction, float speed, float duration) {
        this.direction = direction;
        this.speed = speed;
        this.duration = duration;
        attack = true;
        StartCoroutine(Timer());
    }

    public void Hit() {
        pooler.Despawn(this.gameObject);
    }

    private IEnumerator Timer() {
        yield return new WaitForSecondsRealtime(duration);
        attack = false;
        pooler.Despawn(this.gameObject);
    }

    private void OnDisable() {
        pooler.Despawn(gameObject);
    }
}
