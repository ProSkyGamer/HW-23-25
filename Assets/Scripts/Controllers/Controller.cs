namespace Controllers
{
    public abstract class Controller
    {
        public bool IsActive { get; private set; }

        public float Downtime { get; private set; }

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

            Downtime += deltaTime;

            UpdateInternal(deltaTime);
        }

        protected abstract void UpdateInternal(float deltaTime);

        protected void ResetDowntime()
        {
            Downtime = 0;
        }
    }
}