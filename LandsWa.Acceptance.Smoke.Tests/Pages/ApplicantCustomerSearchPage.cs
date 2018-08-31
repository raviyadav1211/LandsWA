using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsWa.Acceptance.Smoke.Tests.Pages
{
    internal class ApplicantCustomerSearchPage : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//h1[contains(text(),'Assign Applicant/Customer to the New Case')]");
        protected string staticPageHeading = "//h2[contains(text(),'Search for Applicant')]";
        protected string enabledContinueButton = "//button[contains(text(),'Continue')]";

        private IWebDriver _driver;
        ApplicantSearch applicantSearch;

        public ApplicantCustomerSearchPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            applicantSearch = new ApplicantSearch(_driver);
        }

        public ApplicantCustomerSearchPage SearchAnApplicantWithName(string name)
        {
            applicantSearch.EnterFirstName(name)
                .ClickApplyButton();
            return this;
        }

        public ApplicantCustomerSearchPage SelectTheApplicantFromSearchResultWithName(string name)
        {
            applicantSearch.SelectApplicantFromResults(name);
            return this;
        }

        public ApplicantDetailsMileStone Continue()
        {
            GetElementByXpath(staticPageHeading).Click();
            GetElementByXpath(enabledContinueButton).Click();
            return new ApplicantDetailsMileStone(_driver);
        }
    }

    internal class ApplicantSearch : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//h2[contains(text(),'Search for Applicant')]");
        private IWebDriver _driver;
        protected string staticPageHeading = "//h2[contains(text(),'Search for Applicant')]";
        protected string firstNameField = "//*[text()='First Name']/../..//input";
        protected string disabledApplyButton = "//div[@class='Button---disabled_btn_glass']/following-sibling::button[text()='Apply']";
        protected string enabledApplyButton = "//button[contains(text(),'Apply')]";

        protected string searchResultText = "//h2[text()='Search Results']";
        protected string searchResultFirstColumnHeading = "//div[contains(text(),'First Name')]";

        public ApplicantSearch(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public ApplicantSearch EnterFirstName(string firstName)
        {
            GetElementByXpath(firstNameField).SendKeys(firstName);
            return this;
        }

        public ApplicantSearch ClickApplyButton()
        {
            GetElementByXpath(staticPageHeading).Click();
            GetElementByXpath(enabledApplyButton).Click();
            return this;
        }

        public bool IsApplicantSearchResultEmpty()
        {
            IWebElement ele;
            try
            {
                ele = GetElementByXpath(searchResultFirstColumnHeading);
            }
            catch(Exception e)
            {
                Console.WriteLine("Search Result is empty " + e.Message);
                return false;
            }
            return true;
        }

        public bool ApplicantPresentInSearchResult(string name)
        {
            IWebElement ele;
            try
            {
                ele = GetElementByXpath($"(//p[contains(text(),'{name}')])[1]");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Search Result does not contain the name - {name} " + e.Message);
                return false;
            }
            return true;
        }

        public void SelectApplicantFromResults(string name)
        {
            
            IWebElement ele = null;
            try
            {
                ele = GetElementByXpath($"//p[contains(text(),'{name}')]/../../td[1]/div");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Search Result does not contain the name - {name}");
            }
            ele.Click();
        }
    }

    internal class CustomerSearch : BasePage
    {
        protected override By IsPageLoadedBy => By.XPath("//h1[contains(text(),'//h2[contains(text(),'Search for Customer')]");
        private IWebDriver _driver;
        protected string staticPageHeading = "//h2[contains(text(),'Search for Customer')]";
        protected string firstNameField = "//h2[contains(text(),'Search for Customer')]/..//*[text()='First Name']/../..//input";
        protected string disabledApplyButton = "//h2[contains(text(),'Search for Customer')]/..//div[@class='Button---disabled_btn_glass']/following-sibling::button[text()='Apply']";
        protected string enabledApplyButton = "//h2[contains(text(),'Search for Customer')]/..//button[contains(text(),'Apply')]";

        protected string searchResultText = "//h2[contains(text(),'Search for Customer')]/../following-sibling::div//h2[text()='Search Results']";
        protected string searchResultFirstColumnHeading = "(//table)[2]//div[contains(text(),'First Name')]";

        public CustomerSearch(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }
        public CustomerSearch EnterFirstName(string firstName)
        {
            GetElementByXpath(firstNameField).SendKeys(firstName);
            return this;
        }

        public CustomerSearch ClickApplyButton()
        {
            GetElementByXpath(staticPageHeading).Click();
            GetElementByXpath(enabledApplyButton).Click();
            return this;
        }

        public bool IsApplicantSearchResultEmpty()
        {
            IWebElement ele;
            try
            {
                ele = GetElementByXpath(searchResultFirstColumnHeading);
            }
            catch (Exception e)
            {
                Console.WriteLine("Search Result is empty " + e.Message);
                return false;
            }
            return true;
        }

        public bool ApplicantPresentInSearchResult(string name)
        {
            IWebElement ele;
            try
            {
                ele = GetElementByXpath($"((//*[text()='Search Results'])[2]/..//p[contains(text(),'{name}')])[1]");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Search Result does not contain the name - {name} " + e.Message);
                return false;
            }
            return true;
        }

        public void SelectApplicantFromResults(string name)
        {

            IWebElement ele = null;
            try
            {
                ele = GetElementByXpath($"((//*[text()='Search Results'])[2]/..//p[contains(text(),'{name}')])[1]/../../td/div");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Search Result does not contain the name - {name}");
            }
            ele.Click();
        }
    }
}
