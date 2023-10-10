using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0162


namespace DA_Assets.Shared.Extensions
{
    public static class MonoBehExtensions
    {
        public static bool SaveAsPrefabAsset<T>(this GameObject go, string localPath, out T savedPrefab, out Exception ex) where T : MonoBehaviour
        {
            if (go == null)
            {
                ex = new NullReferenceException("GameObject is null.");
                savedPrefab = null;
                return false;
            }

#if UNITY_EDITOR
            GameObject prefabGo = UnityEditor.PrefabUtility.SaveAsPrefabAsset(go, localPath, out bool success);

            if (prefabGo.TryGetComponent<T>(out T prefabComponent))
            {
                ex = null;
                savedPrefab = prefabComponent;
                return true;
            }
            else
            {
                ex = new Exception($"Can't get Type '{typeof(T).Name}' from GameObject '{prefabGo.name}'.");
                savedPrefab = null;
                return false;
            }
#endif
            ex = new Exception("Unsupported in not-Editor mode.");
            savedPrefab = null;
            return false;
        }

        public static bool IsPartOfAnyPrefab(this UnityEngine.Object go)
        {
            if (go == null)
                return false;
#if UNITY_EDITOR
            return UnityEditor.PrefabUtility.IsPartOfAnyPrefab(go);
#endif
            return false;
        }
        public static bool IsExistsOnScene<T>() where T : MonoBehaviour
        {
            int count = MonoBehaviour.FindObjectsOfType<T>().Length;
            return count != 0;
        }

        public static List<T> GetChildren<T>(this Transform aParent) where T : MonoBehaviour
        {
            List<T> children = new List<T>();
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(aParent);
            while (queue.Count > 0)
            {
                Transform c = queue.Dequeue();
                if (c.TryGetComponent<T>(out T component))
                {
                    children.Add(component);
                }
                foreach (Transform t in c)
                    queue.Enqueue(t);
            }
            return children;
        }

        public static IEnumerator WaitForFrames(this UnityEngine.Object @object, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        /// <summary>
        /// Removes all childs from Transform.
        /// <para><see href="https://www.noveltech.dev/unity-delete-children/"/></para>
        /// </summary>
        public static int ClearChilds(this Transform transform)
        {
            int childCount = transform.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                transform.GetChild(i).gameObject.Destroy();
            }

            return childCount;
        }

        /// <summary>
        /// Destroying Unity GameObject, but as an extension.
        /// <para>Works in Editor and Playmode.</para>
        /// </summary>
        /// <summary>
        public static bool Destroy(this UnityEngine.Object unityObject)
        {
            try
            {
#if UNITY_EDITOR
                UnityEngine.Object.DestroyImmediate(unityObject);
#else
                UnityEngine.Object.Destroy(unityObject);
#endif
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Destroying script of Unity GameObject, but as an extension.
        /// <para>Works in Editor and Playmode.</para>
        /// </summary>
        public static bool Destroy(this UnityEngine.Component unityComponent)
        {
            try
            {
#if UNITY_EDITOR
                UnityEngine.Object.DestroyImmediate(unityComponent);
#else
                UnityEngine.Object.Destroy(unityComponent);
#endif
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="component"></param>
        /// <returns>Returns whether a component of the input type has been added.</returns>
        public static bool TryAddComponent<T>(this GameObject gameObject, out T component) where T : UnityEngine.Component
        {
            if (gameObject.TryGetComponent(out component))
            {
                return false;
            }
            else
            {
                component = gameObject.AddComponent<T>();
                return true;
            }
        }

        public static bool TryGetComponent<T>(this GameObject gameObject, out T component) where T : UnityEngine.Component
        {
            try
            {
                component = gameObject.GetComponent<T>();
                string _ = component.name;
                return true;
            }
            catch
            {
                component = default;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="graphic">Found or added graphic component.</param>
        /// <returns>Returns whether a component of the input type has been added.</returns>
        public static bool TryAddGraphic<T>(this GameObject gameObject, out T graphic) where T : Graphic
        {
            if (gameObject.TryGetComponent(out graphic))
            {
                return false;
            }
            else if (gameObject.TryGetComponent(out Graphic _graphic))
            {
                return false;
            }
            else
            {
                graphic = gameObject.AddComponent<T>();
                return true;
            }
        }
        public static bool TryDestroyComponent<T>(this GameObject gameObject) where T : UnityEngine.Component
        {
            if (gameObject.TryGetComponent(out T component))
            {
                component.Destroy();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Marks target object as dirty, but as an extension.
        /// </summary>
        /// <param name="target">The object to mark as dirty.</param>
        public static void SetDirty(this UnityEngine.Object target)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(target);
#endif
        }

        public static GameObject CreateEmptyGameObject(Transform parent = null)
        {
            GameObject tempGO = new GameObject();
            GameObject emptyGO;

            if (parent == null)
            {
                emptyGO = UnityEngine.Object.Instantiate(tempGO);
            }
            else
            {
                emptyGO = UnityEngine.Object.Instantiate(tempGO, parent);
            }

            tempGO.Destroy();
            return emptyGO;
        }
    }
}