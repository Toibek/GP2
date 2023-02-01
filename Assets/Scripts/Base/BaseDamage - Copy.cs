using UnityEngine;

namespace Base
{
    public class BaseDamage : MonoBehaviour
    {

        [SerializeField] private bool broken;
        //[SerializeField] private string direction;
        public enum direction { Left, Right, Up, Dowm };

        public direction SetDirection;
    
        private void Update()
        {
            Debug.Log(SetDirection.ToString());
        }

        void BreakStationPart()
        { 
            broken = true;
        }
    }
}
