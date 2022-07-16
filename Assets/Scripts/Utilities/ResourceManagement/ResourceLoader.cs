using UnityEngine;

namespace Utilities.ResourceManagement
{
    public static class ResourceLoader
    {
        public static Sprite LoadSprite(ResourcePath path) =>
            LoadObject<Sprite>(path);

        public static GameObject LoadPrefab(ResourcePath path) =>
            LoadObject<GameObject>(path);

        public static TObject LoadPrefab<TObject>(ResourcePath path)
        {
            var prefab = LoadPrefab(path);
            var viewObject = Object.Instantiate(prefab);

            var component = viewObject.GetComponent<TObject>();
            return component;
        }
        
        public static TObject LoadPrefabAsChild<TObject>(ResourcePath path, Transform transform)
        {
            var prefab = LoadPrefab(path);
            var viewObject = Object.Instantiate(prefab, transform);

            var component = viewObject.GetComponent<TObject>();
            return component;
        }

        public static TObject LoadObject<TObject>(ResourcePath path) where TObject : Object =>
            Resources.Load<TObject>(path.PathToResource);
    }
}