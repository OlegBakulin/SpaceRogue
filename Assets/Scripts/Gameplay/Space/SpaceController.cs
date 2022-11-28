using System.Linq;
using Abstracts;
using Gameplay.Space.Planet;
using Scriptables.Space;
using Utilities.ResourceManagement;

namespace Gameplay.Space
{
    public class SpaceController : BaseController
    {
        private readonly ResourcePath _configPath = new(Constants.Configs.Space.SpaceConfig);

        private readonly ResourcePath _starSpawnConfigPath = new(Constants.Configs.Space.DefaultStarSpawn);
        private readonly ResourcePath _planetSpawnConfigPath = new(Constants.Configs.Space.DefaultPlanetSpawn);
        private readonly SpaceObjectFactory _spaceObjectFactory;

        public SpaceController()
        {
            var config = ResourceLoader.LoadObject<SpaceConfig>(_configPath);
            var starSpawnConfig = ResourceLoader.LoadObject<StarSpawnConfig>(_starSpawnConfigPath);
            var planetSpawnConfig = ResourceLoader.LoadObject<PlanetSpawnConfig>(_planetSpawnConfigPath);

            _spaceObjectFactory = new SpaceObjectFactory(starSpawnConfig, planetSpawnConfig);

            //TODO Replace with map generation
            foreach (var starSpawnPoint in config.StarSpawnPoints)
            {
                var (star, planetControllers) = _spaceObjectFactory.CreateStarSystem(starSpawnPoint);
                AddController(star);
                AddPlanetControllers(planetControllers);
            }
        }

        private void AddPlanetControllers(PlanetController[] planetControllers)
        {
            if (!planetControllers.Any()) return;
            foreach (var planet in planetControllers)
            {
                AddController(planet);
            }
        }
    }
}