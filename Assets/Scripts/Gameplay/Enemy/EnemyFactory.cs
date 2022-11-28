using Gameplay.Player;
using Scriptables.Enemy;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyFactory
    {
        private readonly EnemyConfig _config;
        
        public EnemyFactory(EnemyConfig config)
        {
            _config = config;
        }

        public EnemyController CreateEnemy(Vector3 spawnPosition, PlayerController playerController) => new(_config, CreateEnemyView(spawnPosition), playerController);

        private EnemyView CreateEnemyView(Vector3 spawnPosition) =>
            Object.Instantiate(_config.Prefab, spawnPosition, Quaternion.identity);
    }
}