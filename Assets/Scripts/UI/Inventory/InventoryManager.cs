using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public int hpPotion_Small_cnt = 0;
    public int hpPotion_Large_cnt = 0;
    public int mpPotion_Small_cnt = 0;
    public int mpPotion_Large_cnt = 0;
    public int invisibilityPotion_cnt = 0;

    public InventoryUI inventoryUI;

    public void InventoryItem(Define.Item itemType, int num)
    {
        switch ((int)itemType)
        {
            case 0:
                hpSmallCnt++;
                break;
            case 1:
                hpLargeCnt++;
                break;
            case 2:
                mpSmallCnt++;
                break;
            case 3:
                mpLargeCnt++;
                break;
            case 4:
                invinsibilityCnt++;
                break;
            default:
                break;
        }
    }

    public int hpSmallCnt
    {
        get { return hpPotion_Small_cnt; }
        set
        {
            if(value != hpPotion_Small_cnt)
            {
                hpPotion_Small_cnt = value;
                if(hpPotion_Small_cnt <= 0)
                {
                    hpPotion_Small_cnt = 0;
                }
                UpdateItemCntText();
            }
        }
    }

    public int hpLargeCnt
    {
        get { return hpPotion_Large_cnt; }
        set
        {
            if (value != hpPotion_Large_cnt)
            {
                hpPotion_Large_cnt = value;
                if (hpPotion_Large_cnt <= 0)
                {
                    hpPotion_Large_cnt = 0;
                }
                UpdateItemCntText();
            }
        }
    }

    public int mpSmallCnt
    {
        get { return mpPotion_Small_cnt; }
        set
        {
            if (value != mpPotion_Small_cnt)
            {
                mpPotion_Small_cnt = value;
                if (mpPotion_Small_cnt <= 0)
                {
                    mpPotion_Small_cnt = 0;
                }
                UpdateItemCntText();
            }
        }
    }

    public int mpLargeCnt
    {
        get { return mpPotion_Large_cnt; }
        set
        {
            if (value != mpPotion_Large_cnt)
            {
                mpPotion_Large_cnt = value;
                if (mpPotion_Large_cnt <= 0)
                {
                    mpPotion_Large_cnt = 0;
                }
                UpdateItemCntText();
            }
        }
    }

    public int invinsibilityCnt
    {
        get { return invisibilityPotion_cnt; }
        set
        {
            if (value != invisibilityPotion_cnt)
            {
                invisibilityPotion_cnt = value;
                if (invisibilityPotion_cnt <= 0)
                {
                    invisibilityPotion_cnt = 0;
                }
                UpdateItemCntText();
            }
        }
    }

    public void UpdateItemCntText()
    {
        Debug.Log("upd");
    }

}
