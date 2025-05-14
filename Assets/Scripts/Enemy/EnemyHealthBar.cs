using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    [SerializeField] private Slider slider;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float offset;
    
    void Start() {
        target = FirstPersonMain.instance.transform;
    }

    public void UpdateHealthBar(float healthMax, float healthCurrent) {
        slider.value = healthMax / healthCurrent;
    }

    void Update() {
        transform.rotation = cam.transform.rotation;
        transform.position = target.position + new Vector3(0, offset, 0);
    }
}