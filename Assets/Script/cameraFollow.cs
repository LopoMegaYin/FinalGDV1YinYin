using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
   
        public Transform playerTransform;
        public float verticalOffset = 2f; // ���������Ϸ��Ĵ�ֱ����
        public float verticalOffsetMax = 4f; // ��������֮������ֱ����
        public float verticalOffsetMin = 1f; // ��������֮�����С��ֱ����
        public float horizontalOffset = 0f; // ��������֮���ˮƽ����
        public float minX = -5f; // ��ҵ���Сx���꣬������������ƶ�
        public float maxX = 5f; // ��ҵ����x���꣬������������ƶ�
        public float minDistanceY = 5;
        public float smoothTime = 0.002f;
        private Vector3 targetPosition; // �����Ŀ��λ��

        private void LateUpdate()
        {
            if (playerTransform == null)
                return;

            // ��ȡ��ҵ�����
            Vector3 playerPosition = playerTransform.position;

            // ���������Ŀ��λ��
            targetPosition = new Vector3(playerPosition.x + horizontalOffset,
                                         Mathf.Clamp(playerPosition.y + verticalOffset, playerPosition.y + verticalOffsetMin, playerPosition.y + verticalOffsetMax),
                                         transform.position.z);

            // �����ҵ�x���곬����Χ���򲻸ı������ˮƽλ��
            if (playerPosition.x < minX || playerPosition.x > maxX)
            {
                targetPosition.x = transform.position.x;
            }
        // �����ҵ�y���곬����Χ���򲻸ı��������ֱλ��
            if (Mathf.Abs(targetPosition.y - playerPosition.y) < minDistanceY)
        {
            targetPosition.y = transform.position.y;
            }
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);

    }


}
