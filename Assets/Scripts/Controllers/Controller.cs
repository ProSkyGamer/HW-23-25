namespace Controllers
{
    public abstract class Controller
    {
        public bool IsActive { get; private set; }

        public virtual void Enable()
        {
            IsActive = true;
        }

        public virtual void Disable()
        {
            IsActive = false;
        }

        public void Update(float deltaTime)
        {
            if (IsActive == false)
                return;

            UpdateInternal(deltaTime);
        }

        protected abstract void UpdateInternal(float deltaTime);
    }
}