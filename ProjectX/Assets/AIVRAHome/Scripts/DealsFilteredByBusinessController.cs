using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealsFilteredByBusinessController : MonoBehaviour
{
    [SerializeField] private GameObject dealsMapCover, businessDeals, customerDeals;
    [SerializeField] private DealsController dealsController;
    [SerializeField] private DealCouponPopulator[] couponPopulators;
    [SerializeField] private InputField search;
    public List<Deal> filteredDeals = new List<Deal>();
    private int dealIndex = 0;
    private int totalDeals;

    public void GetListOfFilteredDeals(string businessName)
    {
        filteredDeals.Clear();
        foreach (Deal deal in dealsController.deals)
        {
            if (deal.businessName.Contains(businessName))
            {
                filteredDeals.Add(deal);
            }
        }
        totalDeals = filteredDeals.Count;
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

    public void OnSearch()
    {
        //GetListOfFilteredDeals(search.text);
        dealsMapCover.SetActive(true);
        businessDeals.SetActive(true);
        customerDeals.SetActive(false);
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
