using UnityEngine;
using System.Collections;
public class CameraFollow : MonoBehaviour
{
    public Transform target; //�J���������Ă����^�[�Q�b�g�̃|�W�V����
    public float smoothing = 5f; //�J�����̂��Ă����X�s�[�h
    Vector3 offset; // �J�����ƃ^�[�Q�b�g�̊Ԃ̋���
    void Start()
    {
        // �J�����ƃ^�[�Q�b�g�̊Ԃ̋������Z�o
        offset = transform.position - target.position;
    }
    // ���̂��������ɌĂяo�����
    void FixedUpdate()
    {
        // �J�����̈ړ���
        Vector3 targetCamPos = target.position + offset;
        // �J�������ړ�����
        transform.position = Vector3.Lerp(transform.position, targetCamPos,
       smoothing * Time.deltaTime);
    }
}

