using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// ���� :
//    Tag   Target,  FlyingStone                                             �� �ֱ� 
//    �ѹ� ������   ������ ��ư ��ױ�
//    �Ѿ�����,  �ٽ�  ���� 
//    �Ѿ����� ����
//    �Ѿ��� ��  �� / ���� ���� 3�� �� ����   : �ڷ�ƾ 
//    ���� ����� Ÿ�� ��ġ��  ������ ��ġ������ ����. 
//    ������ ��ư Ǯ��. 

public enum StoneType
{
    Low,
    Middle,
    High
}
public class TargetStone : MonoBehaviour
{
    public static event Action<StoneType> OnHitByProjectile;
    public static event Action<StoneType> OnKnockDownEvent;
    public Renderer renderer;
    public StoneType stoneType;

    bool isHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            OnHitByProjectile?.Invoke(stoneType);
            isHit = true;
        }
    }

    void Update()
    {
        if (isHit)
        {
            isHit = false;
            if (Math.Abs(transform.rotation.z) < 1)
            {
                StartCoroutine(StoneKnockDown());
            }
        }
    }

    IEnumerator StoneKnockDown()
    {
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        renderer.enabled = false;
        OnKnockDownEvent?.Invoke(stoneType);
        Destroy(gameObject);

    }

}
