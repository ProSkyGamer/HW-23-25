namespace Controllers
{
    public class RandomDirectionController : Controller
    {
        private readonly IPositionMover _positionMover;
        private readonly float _searchRadius;

        public RandomDirectionController(IPositionMover positionMover, float searchRadius)
        {
            _positionMover = positionMover;
            _searchRadius = searchRadius;
        }

        protected override void UpdateInternal(float deltaTime)
        {
            if (_positionMover.IsMoving == false)
                SetRandomDestination();
        }

        private void SetRandomDestination()
        {
            _positionMover.SetDestination(NavMeshHelper.GetRandomLocation(_positionMover.Position, _searchRadius));
        }
    }
}