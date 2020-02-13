using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private KeyCode reloadKey = KeyCode.None;

        // Update is called once per frame
        private void Update()
        {
            if (reloadKey != KeyCode.None && Input.GetKeyDown(reloadKey))
            {
                ReloadCurrentScene();
            }
        }

        public void Reload()
        {
            ReloadCurrentScene();
        }

        static public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}