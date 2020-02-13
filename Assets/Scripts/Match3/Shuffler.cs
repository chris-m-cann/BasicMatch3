using UnityEngine;
using System.Collections;

namespace Match3
{
    public class Shuffler : MonoBehaviour
    {
        public RuntimeGridData grid;

        [SerializeField] private GameObject loadingUI;

        private bool readyToMoveOn = false;
        private bool wantsToMoveOn = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ShuffleBoard();
            }
        }

        public void ReadyToMoveOn(bool ready)
        {
            readyToMoveOn = ready;

            if (readyToMoveOn && wantsToMoveOn)
            {
                NextScene();
            }
        }

        private void NextScene()
        {
            try
            {
                ActivateLoadingUI();

                //ShuffleBoard();
                // make sure no initial matches

                //                UpdateUI();

            }
            finally
            {
                DeactivateLoadingUI();
                wantsToMoveOn = false;
            }
        }

        public void NextScene(int level)
        {
            wantsToMoveOn = true;
            if (readyToMoveOn && wantsToMoveOn)
            {
                NextScene();
            }
        }



        private void ActivateLoadingUI()
        {
            loadingUI.SetActive(true);
        }

        private void DeactivateLoadingUI()
        {
            loadingUI.SetActive(false);
        }


        // this has a potential infinite looop if the last x cells are all blockers as it will never escape thge do-while loop
        // I have no plans to do this and it would make it a little bit more complicated than i need to
        public void ShuffleBoard()
       {
            for (int i = 0; i != grid.cells.LongLength - 1; ++i)
            {
                int x = i % grid.Width;
                int y = i / grid.Width;
                // dont swap blockers
                if (!grid.cells[x, y].IsPassable) continue;

                int x2, y2;
                do
                {
                    int randPos = Random.Range(i, (int)grid.cells.LongLength);
                    x2 = randPos % grid.Width;
                    y2 = randPos / grid.Width;
                } while (!grid.cells[x2, y2].IsPassable);

                SwapTiles(x, y, x2, y2);
            }
            Debug.Log("Shuffle complete");
        }

        // suffers from primative obsession, if this becomes public use Vector2Int to 
        // help make order of parameters less error prone
        private void SwapTiles(int x, int y, int x2, int y2)
        {
            var tmp = grid.cells[x, y].Child;
            grid.SetTileAt(grid.cells[x2, y2].Child, x, y);
            grid.SetTileAt(tmp, x2, y2);
            grid.cells[x, y].Child.transform.localPosition = Vector3.zero;
            grid.cells[x2, y2].Child.transform.localPosition = Vector3.zero;
        }
    }
}