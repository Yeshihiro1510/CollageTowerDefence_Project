using UnityEngine;

namespace Yeshi_Pool
{
    [RequireComponent(typeof(CanvasGroup))]
    public class RectPoolable : MonoPoolable
    {
        private CanvasGroup _alphaGroup;

        private void Awake()
        {
            _alphaGroup = GetComponent<CanvasGroup>();
        }

        public void TurnOn() => _alphaGroup.alpha = 1;
        public void TurnOff() => _alphaGroup.alpha = 0;

        public override void ResetObject()
        {
            transform.SetParent(null);
            _alphaGroup.alpha = 0;
        }

        public override void SetupObject()
        {
            _alphaGroup.alpha = 1;
        }
    }
}