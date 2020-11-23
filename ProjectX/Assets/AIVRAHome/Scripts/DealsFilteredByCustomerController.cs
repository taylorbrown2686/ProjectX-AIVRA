using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealsFilteredByCustomerController : MonoBehaviour
{
    [SerializeField] private GameObject dealsMapCover, customerDeals, businessDeals;
    [SerializeField] private DealsController dealsController;
    [SerializeField] private DealCouponPopulator[] couponPopulators;
    public List<Deal> filteredDeals = new List<Deal>();
    private int dealIndex = 0;
    private int totalDeals;

    public void GetListOfFilteredDeals()
    {
        filteredDeals = new List<Deal>(dealsController.yourDeals);
        totalDeals = filteredDeals.Count;
        Debug.Log(totalDeals);
        DisplayDealList();
    }

    private void DisplayDealList()
    {
        for (int i = 0; i < 3; i++)
        {
            try
            {
                couponPopulators[i].gameObject.SetActive(true);
                couponPopulators[i].PopulateCoupon(filteredDeals[dealIndex + i]);
            }
            catch (ArgumentOutOfRangeException e)
            {
                couponPopulators[i].gameObject.SetActive(false);
            }
        }
    }

    public void SeeMyDeals()
    {
        //GetListOfFilteredDeals();
        dealsMapCover.SetActive(true);
        customerDeals.SetActive(true);
        businessDeals.SetActive(false);
    }

    public void LeftArrow()
    {
        dealIndex -= 3;
        if (dealIndex < 0)
        {
            dealIndex = ((totalDeals / 3) * 3);
        }
        Debug.Log(dealIndex);
        DisplayDealList();
    }

    public void RightArrow()
    {
        dealIndex += 3;
        if (dealIndex > totalDeals)
        {
            dealIndex = 0;
        }
        Debug.Log(dealIndex);
        DisplayDealList();
    }
}
