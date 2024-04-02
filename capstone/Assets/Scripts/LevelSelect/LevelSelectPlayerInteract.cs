using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayerInteract : MonoBehaviour
{
    public LayerMask levelLayers;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange, levelLayers);
            foreach(Collider collider in colliderArray) {
                if (collider.TryGetComponent(out LoadScene loadScene)) {
                    loadScene.InteractLoadScene();
                }
            }
        }
    }
}
