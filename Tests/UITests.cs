using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.TableItems;

namespace HospitalSchedulingTests
{
    [TestClass]
    public class TestClass
    {
        private TestStack.White.Application application;
        
        private ListViewRows GetGridRows(ListView Grid)
        {
            ListViewRows Collection = Grid.Rows;
            do
            {
                Grid.ScrollBars.Vertical.ScrollDown();

                Collection.AddRange(Grid.Rows);

            } while (Grid.ScrollBars.Vertical.Value < Grid.ScrollBars.Vertical.MaximumValue);

            return Collection;
        }

        public void SetupApp()
        {
            string outputDir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent.FullName;
            string app_path = outputDir + @"\HospitalScheduling.exe.lnk";

            application = TestStack.White.Application.Launch(app_path);
            //Window mainWindow = application.GetWindow("LoginWindow");
        }

        [TestMethod]
        public void FrontDeskID_Test()
        {
            SetupApp();
            Window mainWindow = application.GetWindow("Login Window");
            
            string Username = "TestUser";

            TextBox loginBox = mainWindow.Get<TextBox>(SearchCriteria.ByAutomationId("LoginBox"));
            loginBox.Text = Username;

            ComboBox cb_Roles = mainWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("RoleCombobox"));
            cb_Roles.Select("Front Desk");

            Button loginBtn = mainWindow.Get<Button>(SearchCriteria.ByText("OK"));
            loginBtn.Click();

            mainWindow = application.GetWindow("FrontDeskView");
            Label WelcomeText = mainWindow.Get<Label>(SearchCriteria.ByAutomationId("WelcomeText"));

            Assert.AreEqual(Username, WelcomeText.Text);

            Console.Write("Logged in with username " + Username);

            
            var Grid = mainWindow.Get<ListView>(SearchCriteria.ByAutomationId("PatientSchedulingGrid"));

            Window PatientWindow;

            int index = 3;
            foreach (var row in Grid.Rows)
            {                           
                var DetailsButton = mainWindow.Get<Button>(SearchCriteria.Indexed(index++));

                DetailsButton.Click();

                PatientWindow = application.GetWindow("PatientScheduling");
                TextBox name = PatientWindow.Get<TextBox>(SearchCriteria.ByAutomationId("PatientName"));

                Assert.AreEqual(name.Text, row.Cells["PatientName"].Text);

                PatientWindow.Close();                                
            }

            Tab tabControl = mainWindow.Get<Tab>(SearchCriteria.ByClassName("TabControl"));
            tabControl.SelectTabPage("Appointments");

            CheckBox ShowUpsFilter = mainWindow.Get<CheckBox>(SearchCriteria.ByAutomationId("ShowUpsOnly"));
            Grid = mainWindow.Get<ListView>(SearchCriteria.ByAutomationId("AppointmentsGrid"));

            index = 3;
            foreach (var row in Grid.Rows)
            {
                var DetailsButton = mainWindow.Get<Button>(SearchCriteria.Indexed(index++));

                DetailsButton.Click();

                PatientWindow = application.GetWindow("PatientScheduling");
                TextBox name = PatientWindow.Get<TextBox>(SearchCriteria.ByAutomationId("PatientName"));

                Assert.AreEqual(name.Text, row.Cells["PatientName"].Text);

                PatientWindow.Close();
            }

            application.Close();
        }


        [TestMethod]
        public void Supervisor_Test()
        {
            SetupApp();
            Window mainWindow = application.GetWindow("Login Window");

            ComboBox cb_Roles = mainWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("RoleCombobox"));
            cb_Roles.Select("Supervisor");

            Button loginBtn = mainWindow.Get<Button>(SearchCriteria.ByText("OK"));
            loginBtn.Click();

            mainWindow = application.GetWindow("SpecialityView");

            Label date = mainWindow.Get<Label>(SearchCriteria.ByAutomationId("txt_date"));

            Assert.AreEqual(date.Text, DateTime.Now.ToString("MMMM dd, yyyy"));

            
        }

    }
}
