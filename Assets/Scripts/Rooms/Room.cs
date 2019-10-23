using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public void RoomEnter() {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition() {
        CameraFollow cameraInstance = CameraFollow.Instance;

        cameraInstance.roomBounds = this;
        cameraInstance.IsFollowing = false;

        PlayerMovement player = cameraInstance.GetComponent<FollowLeader>().partyMembers[0].GetComponent<PlayerMovement>();
        player.isActive = false;        

        Vector3 destination = cameraInstance.GetDestination();
        Tween cameraTween = cameraInstance.transform.DOMove(destination, .5f);
        yield return cameraTween.WaitForCompletion();

        player.isActive = true;
        player.ForceMove(Vector2.zero);
        cameraInstance.IsFollowing = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x, GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y));
    }
}
