using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class EnablePauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu();
            }
        }

        public void TogglePauseMenu()
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
    }
}