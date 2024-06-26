﻿using OpenQA.Selenium;
using Tools;

namespace Core.Pages
{
    /// <summary>
    /// AvaTrade home page
    /// </summary>
    public class ATMainPage : BasePage
    {
        private By logoBy => By.Id("com.avatrade.mobile:id/ava_main_toolbar_icon");

        public override void WaitForPageLoading()
        {
            WaitForVisible(logoBy, WaitTime.ThirtySec);
        }
    }
}
