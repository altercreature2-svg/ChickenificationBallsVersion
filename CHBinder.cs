using System.Collections;
using UnityEngine;

namespace Chickenification {

    public class CHBinder : MonoBehaviour {

        public static void UnitGlad() {

            if (!instance) {

                instance = new GameObject {
                    hideFlags = HideFlags.HideAndDontSave
                }.AddComponent<CHBinder>();
            }
            instance.StartCoroutine(StartUnitgradLate());
        }

        private static IEnumerator StartUnitgradLate()
        {
            var chickenification = AssetBundle.LoadFromMemory(Properties.Resources.chickenification);
            yield return new WaitUntil(() => FindObjectOfType<ServiceLocator>() != null);
            yield return new WaitUntil(() => ServiceLocator.GetService<ISaveLoaderService>() != null);
            yield return new WaitForSeconds(0.5f);
            Mesh cheeken = chickenification.LoadAsset<GameObject>("cheeken").GetComponent<MeshFilter>().mesh;
            foreach (var mesh in Resources.FindObjectsOfTypeAll<MeshFilter>())
            {
                if (mesh && mesh.mesh) mesh.mesh = cheeken;
            }
            foreach (var bendyMesh in Resources.FindObjectsOfTypeAll<SkinnedMeshRenderer>())
            {
                if (bendyMesh && bendyMesh.sharedMesh) bendyMesh.sharedMesh = cheeken
            }
            yield break;
        }

        private static CHBinder instance;
    }
}
