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
            
            string outputDir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent.FullName;
            string app_path = outputDir + @"\HospitalScheduling.exe.lnk";

            application = TestStack.White.Application.Launch(app_path);
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
