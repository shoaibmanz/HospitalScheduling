using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.TabItems;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private TestStack.White.Application application;
        
        public void SetupApp()
        {
            string outputDir = @"C:\Users\Shoaib\Desktop\HospitalScheduling\HospitalScheduling\bin\Debug\HospitalScheduling.exe";

            application = TestStack.White.Application.Launch(outputDir);
            //Window mainWindow = application.GetWindow("LoginWindow");
        }

        [TestMethod]
        public void LoginWindow_Test()
        {
            SetupApp();
            Window mainWindow = application.GetWindow("Login Window");
            Button loginBtn = mainWindow.Get<Button>(SearchCriteria.ByText("OK"));
            loginBtn.Click();

            Console.Write("Logged in");
        }
    }
}
