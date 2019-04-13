using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

public class TankTurretController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float rotationSpeed;
    [SerializeField]
    public int fireRateInMs;
    [SerializeField]
    public float fireTriggerRange;

    public Transform firePos;
    public GameObject bullet;
    public Joystick turretJoyStick;

    private List<IDisposable> subs = new List<IDisposable>();

    private void Start()
    {
        var fireRate = new TimeSpan(0, 0, 0, 0, fireRateInMs);
        var everyFixUpdate = Observable.EveryFixedUpdate();

        subs.Add(everyFixUpdate
            .Where(_ => Mathf.Abs(turretJoyStick.Vertical) > fireTriggerRange || Mathf.Abs(turretJoyStick.Horizontal) > fireTriggerRange)
            .Throttle(fireRate)
            .Subscribe(_ => {
                var b = Instantiate(bullet, firePos.position, firePos.rotation);
                b.GetComponent<Rigidbody>().velocity = transform.forward * 10f;
            }));

        subs.Add(everyFixUpdate
            .Where(_ => turretJoyStick.Vertical != 0f || turretJoyStick.Vertical != 0f)
            .Subscribe(_ => rotate()));
    }

    private void OnDestroy()
    {
        subs.ForEach(sub => sub.Dispose());
    }

    void rotate()
    {
        Vector3 direction = Vector3.forward * turretJoyStick.Vertical + Vector3.right * turretJoyStick.Horizontal;
        direction.y = gameObject.transform.position.y;
        direction.x += gameObject.transform.position.x;
        direction.z += gameObject.transform.position.z;
        gameObject.transform.LookAt(direction);
    }
}
