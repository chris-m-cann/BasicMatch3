using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class ReloadScene : MonoBehaviour
    {
        [SerializeField] private bool resetOnR = true;

        // Update is called once per frame
        private void Update()
        {
            if (resetOnR && Input.GetKeyDown(KeyCode.R))
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
    }
}