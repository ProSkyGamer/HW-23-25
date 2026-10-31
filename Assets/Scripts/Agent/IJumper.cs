namespace Controllers
{
    public interface IJumper
    {
        public bool IsOnNavMeshLink { get; }

        public bool IsJumping { get; }

        public void Jump();
    }
}