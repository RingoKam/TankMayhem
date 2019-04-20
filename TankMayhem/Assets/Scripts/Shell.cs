using UnityEngine;

public class Shell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BulletInteractable"))
        {
            //determine if game object needs to be destroyed
            Destroy(gameObject);
            //Deal damage to Collided Object
            //Unparent visual effects, Instantiate Visual Effects

        }
    }

}
