#region

using System.Collections;
using UnityEngine;

#endregion

namespace Controllers
{
    public abstract class CoroutineController
    {
        public bool IsActive => _coroutine != null;

        private Coroutine _coroutine;

        private readonly MonoBehaviour _coroutineRunner;

        public CoroutineController(MonoBehaviour coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public virtual void Enable()
        {
            _coroutine = _coroutineRunner.StartCoroutine(ProcessController());
        }

        public virtual void Disable()
        {
            _coroutineRunner.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        protected abstract IEnumerator ProcessController();
    }
}