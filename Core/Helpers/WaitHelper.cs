﻿using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;
using Tools;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Core.Helpers
{
    public static class WaitHelper
    {
        private static readonly AndroidDriver<AndroidElement> Driver = AndroidDriver.GetInstance();

        public static void WaitForElementTextChange(By element, string text, int? timeoutInSec = null)
        {
            var timeout = timeoutInSec != null ? timeoutInSec.Value : Config.DefaultTimeoutTimeInSec;
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(driver => Driver.FindElement(element).Text.Equals(text));
            }
            catch (Exception e)
            {
                throw new Exception($"Wait For Element {element} text to change failed: {e.Message}");
            }
        }

        public static void WaitForVisible(By element, int? timeoutInSec = null)
        {
            var timeout = timeoutInSec != null ? timeoutInSec.Value : Config.DefaultTimeoutTimeInSec;
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(ExpectedConditions.ElementIsVisible(element));
            }
            catch (Exception e)
            {
                throw new Exception($"Wait For Element {element} to become visible failed: {e.Message}");
            }
        }

        public static void WaitForDisappear(By element, int? timeoutInSec = null)
        {
            var timeout = timeoutInSec != null ? timeoutInSec.Value : Config.DefaultTimeoutTimeInSec;
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(element));
            }
            catch (Exception e)
            {
                throw new Exception($"Wait For Element {element} to disappear failed: {e.Message}");
            }
        }

        public static void WaitUntilTrue(Func<bool> func, int? timeoutInSec = null)
        {
            var currentTime = DateTime.UtcNow.Second;
            var timeout = timeoutInSec != null ? timeoutInSec.Value : Config.DefaultTimeoutTimeInSec;

            while (!func.Invoke() && ((DateTime.UtcNow.Second - currentTime) < timeout))
            {
                Thread.Sleep(TimeSpan.FromSeconds(WaitTime.OneSec));
            }

            if (!func.Invoke())
            {
                throw new Exception($"Condition didn't become true in {timeout}");
            }
        }
    }
}
