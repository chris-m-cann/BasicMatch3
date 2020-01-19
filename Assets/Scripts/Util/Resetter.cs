using UnityEngine;


namespace Util
{
    public class Resetter : MonoBehaviour
    {

        [SerializeField] private RestablesRuntimeSet resetables;


        private void Start()
        {
            for (int i = resetables.Size - 1; i >= 0; --i)
            {
                resetables[i].Reset();
            }
        }

        public void Register(Resetable resetable)
        {
            resetables.Add(resetable);
        }


        public void Unregister(Resetable resetable)
        {
            resetables.Remove(resetable);
        }
    }

    public interface Resetable
    {
        void Reset();
    }
}