using Content.Server.GameObjects.Components.Weapon;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Server.GameObjects.Components.Projectiles
{
    /// <summary>
    /// Upon colliding with an object this will flash in an area around it
    /// </summary>
    [RegisterComponent]
    public class FlashProjectileComponent : Component, ICollideBehavior
    {
        public override string Name => "FlashProjectile";

        [DataField("range")]
        private float _range = 1.0f;
        [DataField("duration")]
        private float _duration = 8.0f;

        private bool _flashed;

        public override void Initialize()
        {
            base.Initialize();
            // Shouldn't be using this without a ProjectileComponent because it will just immediately collide with thrower
            Owner.EnsureComponent<ProjectileComponent>();
        }

        void ICollideBehavior.CollideWith(IEntity entity)
        {
            if (_flashed)
            {
                return;
            }
            FlashableComponent.FlashAreaHelper(Owner, _range, _duration);
            _flashed = true;
        }

        // Projectile should handle the deleting
        void ICollideBehavior.PostCollide(int collisionCount)
        {
            return;
        }
    }
}
