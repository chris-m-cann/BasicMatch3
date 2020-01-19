using UnityEngine;

namespace Match3
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private LayerMask matchableLayer;
        [SerializeField] private bool touchInput = false;
        [SerializeField] private RuntimeGridData grid;
        [SerializeField] private CoreLogic game;

        private GameObject highlighted;
        private bool inputIsAllowed = true;

        public void SetInputAllowed(bool allowInput)
        {
            inputIsAllowed = allowInput;
        }

        private void Update()
        {
            if (touchInput)
            {
                TouchUpdate();
            }
            else
            {
                MouseUpdate();
            }
        }

        // this isnt strictly needed as we acheive the same thing on touch devices just using the mouse controls but is being kept in case we expand it beyond what mouse can do
        private void TouchUpdate()
        {
            if (Input.touchCount > 0 && inputIsAllowed)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(touch.position), Mathf.Infinity, matchableLayer);

                    if (hit.collider != null)
                    {
                        highlighted = hit.collider.gameObject;
                    }
                }
                else
                {
                    if (highlighted != null)
                    {
                        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(touch.position), Mathf.Infinity, matchableLayer);
                        // on another matchable
                        if (hit.collider != null && hit.collider.gameObject != highlighted)
                        {
                            // TODO(chris) - maybe move this to triggering a "Swap" event. that way this isnt dependent on the core logic and we can test the input independently
                            if (grid.AreAdjacent(highlighted, hit.collider.gameObject))
                            {
                                game.Swap(highlighted, hit.collider.gameObject);

                                highlighted = null;
                            }
                        }


                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    highlighted = null;
                }


            }
        }

        private void MouseUpdate()
        {
            if (Input.GetMouseButton(0) && inputIsAllowed)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, matchableLayer);

                    if (hit.collider != null)
                    {
                        highlighted = hit.collider.gameObject;
                    }
                }
                else
                {
                    if (highlighted != null)
                    {
                        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, matchableLayer);
                        // on another matchable
                        if (hit.collider != null && hit.collider.gameObject != highlighted)
                        {
                            // TODO(chris) - maybe move this to triggering a "Swap" event. that way this isnt dependent on the core logic and we can test the input independently
                            // grid (or whatever picks up the event) can test if they are adjacent
                            if (grid.AreAdjacent(highlighted, hit.collider.gameObject))
                            {
                                game.Swap(highlighted, hit.collider.gameObject);

                                highlighted = null;
                            }
                        }


                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    highlighted = null;
                }


            }
        }

    }
}