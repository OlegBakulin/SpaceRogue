using System.IO;

public static class Constants
{
    public record Configs
    {
        private static readonly string CONFIGS = nameof(Configs);

        public record Player
        {
            private static readonly string PLAYER = Path.Combine(CONFIGS, nameof(Player));

            public static readonly string PlayerConfig = Path.Combine(PLAYER, nameof(PlayerConfig));
        }

        public record Enemy
        {
            private static readonly string ENEMY = Path.Combine(CONFIGS, nameof(Enemy));

            public static readonly string EnemySpawnConfig = Path.Combine(ENEMY, nameof(EnemySpawnConfig));
        }

        public record Space
        {
            private static readonly string SPACE = Path.Combine(CONFIGS, nameof(Space));

            public static readonly string SpaceConfig = Path.Combine(SPACE, nameof(SpaceConfig));
            public static readonly string DefaultStarSpawn = Path.Combine(SPACE, nameof(DefaultStarSpawn));
            public static readonly string DefaultPlanetSpawn = Path.Combine(SPACE, nameof(DefaultPlanetSpawn));
        }
    }

    public record Prefabs
    {
        private static readonly string PREFABS = nameof(Prefabs);

        public record Input
        {
            private static readonly string INPUT = Path.Combine(PREFABS, nameof(Input));

            public static readonly string KeyboardInput = Path.Combine(INPUT, nameof(KeyboardInput));
        }

        public record Gameplay
        {
            private static readonly string GAMEPLAY = Path.Combine(PREFABS, nameof(Gameplay));

            public static readonly string Player = Path.Combine(GAMEPLAY, nameof(Player));
        }

        public record Stuff
        {
            private static readonly string STUFF = Path.Combine(PREFABS, nameof(Stuff));

            public static readonly string GunPoint = Path.Combine(STUFF, nameof(GunPoint));
            public static readonly string Crosshair = Path.Combine(STUFF, nameof(Crosshair));
        }

        public record Canvas
        {
            private static readonly string CANVAS = Path.Combine(PREFABS, nameof(Canvas));

            public static readonly string UICamera = Path.Combine(CANVAS, nameof(UICamera));
            public static readonly string MainCanvas = Path.Combine(CANVAS, nameof(MainCanvas));

            public record Game
            {
                private static readonly string GAME = Path.Combine(CANVAS, nameof(Game));

                public static readonly string StatusBarCanvas = Path.Combine(GAME, nameof(StatusBarCanvas));
                public static readonly string SpeedometerCanvas = Path.Combine(GAME, nameof(SpeedometerCanvas));
                public static readonly string DestroyPlayerCanvas = Path.Combine(GAME, nameof(DestroyPlayerCanvas));
            }
        }
    }
}