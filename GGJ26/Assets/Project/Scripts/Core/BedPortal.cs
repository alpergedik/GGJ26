using UnityEngine;
using UnityEngine.SceneManagement;

public class BedPortal : MonoBehaviour
{
    [SerializeField] private string targetScene = "Dream_Stone";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        SceneManager.LoadScene(targetScene);
    }
}
