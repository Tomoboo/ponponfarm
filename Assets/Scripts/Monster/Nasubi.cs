using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nasubi : MonsterController
{

    new void Start()
    {
        base.Start();
        this.span = 40.0f;
        rate = 2.5f;
        int size = 5;
        var col = gameObject.GetComponent<CapsuleCollider2D>();
        col.size = new Vector3(size, size, size);
    }

    protected override void farmTime()
    {
        second += Time.deltaTime;
        if (second > 60f)
        {
            minute += 1;
            second = 0;
        }
        if (minute > 60)
        {
            hour++;
            minute = 0;
        }
        //�琬���ԂɒB������
        if (second > this.span && Ms1Complete_flg == false)   //�{����second ���@hour
        {
            Ms1Complete_flg = true;
            // �����X�^�[���ƂɈ����ݒ�
            director.GetComponent<GameDirector>().FarmComplete(0);
            // �����_���m���Ŋ��ӂ̗�GET
            if (Random.Range(0, 1000) < dropprate)
                director.GetComponent<GameDirector>().kanshaGet();
        }

        else if (second > (this.span / 2))
        {
            int size = 6;
            isScale = true;
            Vector3 kero = new Vector3(80, 80, 1);
            kero.x = 80;
            kero.y = 80;
            kero.z = 1;
            this.transform.localScale = kero;
            var col = gameObject.GetComponent<CapsuleCollider2D>();
            col.size = new Vector3(size,size,size);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        Enepower_flg = UICanvas.GetComponent<Info_Monsters>().Enepower_flg; // �������X�^�[����
        float spanPercent = span * decreasePoint; //���C�Q�[�W�̌�����

        farmTime();
        base.UseItem1();
        base.UseItem2();
        base.UseItem3();
        base.UseItem4();
        base.Stop();

        if ((detectCollider.item3_flg == false || detectCollider.item4_flg == false) && isStop == false &&
           (GameObject.FindWithTag("item1") == null) && (GameObject.FindWithTag("item2") == null) &&
           (GameObject.FindWithTag("item3") == null) && (GameObject.FindWithTag("item4") == null))
        {
            // �X�g�b�v�^�C�}���Z�b�g
            stoptimer = 0;
            base.Move();
            //�C���X�g���E�����]��
            if (currentpos.x < targetpos.x)
                Renderer.flipX = true;  //�E����
            else
                Renderer.flipX = false;//������
            /*if (isScale == false)
            {
                if (targetpos.x > 464.76){if (num == 1)isStop = true;
                    num = -1;
                }
                else if (targetpos.x < -464.76){if (num == -1)isStop = true;
                    num = 1;
                }
            }
            else
            {
                if (targetpos.x > 386){if (num == 1)isStop = true;
                    num = -1;
                }else if (targetpos.x < -386){if (num == -1)isStop = true;
                    num = 1;
                }
            }*/
        }

        //���C�Q�[�W��50���ȏ�݈̂琬���x���㏸
        if (Enepower_flg == true)
            {
                second += Time.deltaTime * 1.002f;
            }

            //�琬���Ԃ�10���i�ނɂ�Č��C�Q�[�W��10��������
            if (second - oldSecond > spanPercent)
            {
                nowGauge -= 10;
                oldSecond = second;
                if (nowGauge < 0)
                    nowGauge = 0;
            }

    }
}
