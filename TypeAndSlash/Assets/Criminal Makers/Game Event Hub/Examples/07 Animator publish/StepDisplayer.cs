using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    public class StepDisplayer : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public bool leftStep = false;

        private void OnEnable()
        {
            Hide();
            GameEventHub.Bind(this);
        }

        private void OnDisable()
        {
            GameEventHub.Unbind(this);
        }

        [OnGameEvent]
        public void OnStep(OnStep e)
        {
            if (e.isLeftFoot != leftStep) return;

            meshRenderer.enabled = true;

            Invoke(nameof(Hide), 0.1f);
        }

        private void Hide()
        {
            meshRenderer.enabled = false;
        }
    }
}