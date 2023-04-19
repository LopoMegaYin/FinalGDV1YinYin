using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
   
        public Transform playerTransform;
        public float verticalOffset = 2f; // 相机在玩家上方的垂直距离
        public float verticalOffsetMax = 4f; // 相机和玩家之间的最大垂直距离
        public float verticalOffsetMin = 1f; // 相机和玩家之间的最小垂直距离
        public float horizontalOffset = 0f; // 相机和玩家之间的水平距离
        public float minX = -5f; // 玩家的最小x坐标，相机不会向左移动
        public float maxX = 5f; // 玩家的最大x坐标，相机不会向右移动
        public float minDistanceY = 5;
        public float smoothTime = 0.002f;
        private Vector3 targetPosition; // 相机的目标位置

        private void LateUpdate()
        {
            if (playerTransform == null)
                return;

            // 获取玩家的坐标
            Vector3 playerPosition = playerTransform.position;

            // 计算相机的目标位置
            targetPosition = new Vector3(playerPosition.x + horizontalOffset,
                                         Mathf.Clamp(playerPosition.y + verticalOffset, playerPosition.y + verticalOffsetMin, playerPosition.y + verticalOffsetMax),
                                         transform.position.z);

            // 如果玩家的x坐标超出范围，则不改变相机的水平位置
            if (playerPosition.x < minX || playerPosition.x > maxX)
            {
                targetPosition.x = transform.position.x;
            }
        // 如果玩家的y坐标超出范围，则不改变相机的竖直位置
            if (Mathf.Abs(targetPosition.y - playerPosition.y) < minDistanceY)
        {
            targetPosition.y = transform.position.y;
            }
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);

    }


}
