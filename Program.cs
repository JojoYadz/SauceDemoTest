using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

class SauceDemoTest
{
    static void Main()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--incognito");


        IWebDriver driver = new ChromeDriver(options);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        try
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            wait.Until(d => d.Url.Contains("inventory.html"));
            Console.WriteLine("Login successful.");

            driver.FindElement(By.XPath("//div[text()='Sauce Labs Bolt T-Shirt']")).Click();

            wait.Until(d => d.FindElement(By.ClassName("inventory_details_name")).Displayed);
            Console.WriteLine("T-shirt details page displayed.");

            driver.FindElement(By.ClassName("btn_primary")).Click();

            wait.Until(d => d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");
            Console.WriteLine("T-shirt added to cart.");

            driver.FindElement(By.ClassName("shopping_cart_link")).Click();

            wait.Until(d => d.Url.Contains("cart.html"));
            Console.WriteLine("Cart page displayed.");

            IWebElement cartItem = driver.FindElement(By.ClassName("inventory_item_name"));
            Console.WriteLine("Item in cart: " + cartItem.Text);

            driver.FindElement(By.ClassName("checkout_button")).Click();

            wait.Until(d => d.Url.Contains("checkout-step-one.html"));
            Console.WriteLine("Checkout information page displayed.");

            driver.FindElement(By.Id("first-name")).SendKeys("John");
            driver.FindElement(By.Id("last-name")).SendKeys("Doe");
            driver.FindElement(By.Id("postal-code")).SendKeys("12345");
            driver.FindElement(By.ClassName("cart_button")).Click();

            wait.Until(d => d.Url.Contains("checkout-step-two.html"));
            Console.WriteLine("Order summary page displayed.");

            driver.FindElement(By.ClassName("cart_button")).Click();

            wait.Until(d => d.Url.Contains("checkout-complete.html"));
            Console.WriteLine("Order confirmation page displayed.");

            driver.FindElement(By.ClassName("bm-burger-button")).Click();
            wait.Until(d => d.FindElement(By.Id("logout_sidebar_link")).Displayed);
            driver.FindElement(By.Id("logout_sidebar_link")).Click();

            wait.Until(d => d.Url.Contains("saucedemo.com"));
            Console.WriteLine("Successfully logged out.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Test failed: " + ex.Message);
        }
        finally
        {
            driver.Quit();
        }
    }
}
