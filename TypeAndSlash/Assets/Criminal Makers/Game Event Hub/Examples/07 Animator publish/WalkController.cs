using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    public class WalkController : MonoBehaviour
    {
        private static readonly int SpeedParameter = Animator.StringToHash("speed");
        public Animator animator;
        public KeyCode walkKey = KeyCode.Space;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(walkKey)) animator.CrossFade("Walking", 0.2f);

            if (Input.GetKeyUp(walkKey)) animator.CrossFade("Idle", 0.2f);

            var randomSpeed = Random.Range(0.5f, 1.5f);
            animator.SetFloat(SpeedParameter, randomSpeed);
        }
    }
}