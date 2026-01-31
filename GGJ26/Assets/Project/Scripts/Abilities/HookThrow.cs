using UnityEngine;

public class HookThrow : MonoBehaviour
{
    public GameObject hook;
    private Quaternion hookRotation = Quaternion.Euler(1f, 1.7f, 0f) ;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ShootHook();
        }
        Hook.playerPosition = transform;
        
    }

    void ShootHook()
    {
        Instantiate(hook,transform.position, hookRotation);
        hook.transform.rotation = hookRotation;
    }
}
