﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 12.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace iOrderUiTest
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public partial class UIMap
    {
        
        /// <summary>
        /// RecordedMethod1 - Use 'RecordedMethod1Params' to pass parameters into this method.
        /// </summary>
        public void RecordedMethod1()
        {
            #region Variable Declarations
            WinEdit uIAddressandsearchbarEdit = this.UINewTabGoogleChromeWindow.UIItemGroup.UIAddressandsearchbarEdit;
            WinClient uIChromeLegacyWindowClient = this.UINewTabGoogleChromeWindow.UIChromeLegacyWindowWindow.UIChromeLegacyWindowClient;
            #endregion

            // Launch '%ProgramFiles%\Google\Chrome\Application\chrome.exe'
            ApplicationUnderTest uINewTabGoogleChromeWindow = ApplicationUnderTest.Launch(this.RecordedMethod1Params.UINewTabGoogleChromeWindowExePath, this.RecordedMethod1Params.UINewTabGoogleChromeWindowAlternateExePath);

            // Type '********' in 'Address and search bar' text box
            Keyboard.SendKeys(uIAddressandsearchbarEdit, this.RecordedMethod1Params.UIAddressandsearchbarEditSendKeys, true);

            // Click 'Chrome Legacy Window' client
            Mouse.Click(uIChromeLegacyWindowClient, new Point(1080, 8));

            // Click 'Chrome Legacy Window' client
            Mouse.Click(uIChromeLegacyWindowClient, new Point(1210, 725));
        }
        
        #region Properties
        public virtual RecordedMethod1Params RecordedMethod1Params
        {
            get
            {
                if ((this.mRecordedMethod1Params == null))
                {
                    this.mRecordedMethod1Params = new RecordedMethod1Params();
                }
                return this.mRecordedMethod1Params;
            }
        }
        
        public UINewTabGoogleChromeWindow UINewTabGoogleChromeWindow
        {
            get
            {
                if ((this.mUINewTabGoogleChromeWindow == null))
                {
                    this.mUINewTabGoogleChromeWindow = new UINewTabGoogleChromeWindow();
                }
                return this.mUINewTabGoogleChromeWindow;
            }
        }
        #endregion
        
        #region Fields
        private RecordedMethod1Params mRecordedMethod1Params;
        
        private UINewTabGoogleChromeWindow mUINewTabGoogleChromeWindow;
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'RecordedMethod1'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class RecordedMethod1Params
    {
        
        #region Fields
        /// <summary>
        /// Launch '%ProgramFiles%\Google\Chrome\Application\chrome.exe'
        /// </summary>
        public string UINewTabGoogleChromeWindowExePath = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
        
        /// <summary>
        /// Launch '%ProgramFiles%\Google\Chrome\Application\chrome.exe'
        /// </summary>
        public string UINewTabGoogleChromeWindowAlternateExePath = "%ProgramFiles%\\Google\\Chrome\\Application\\chrome.exe";
        
        /// <summary>
        /// Type '********' in 'Address and search bar' text box
        /// </summary>
        public string UIAddressandsearchbarEditSendKeys = "XQGxZarsUFSPSI6iz6BrIq0YRpGILEa8";
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class UINewTabGoogleChromeWindow : WinWindow
    {
        
        public UINewTabGoogleChromeWindow()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "New Tab - Google Chrome";
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "Chrome_WidgetWin_1";
            this.WindowTitles.Add("New Tab - Google Chrome");
            this.WindowTitles.Add("Documents Index - My ASP.NET Application - Google Chrome");
            #endregion
        }
        
        #region Properties
        public UIItemGroup UIItemGroup
        {
            get
            {
                if ((this.mUIItemGroup == null))
                {
                    this.mUIItemGroup = new UIItemGroup(this);
                }
                return this.mUIItemGroup;
            }
        }
        
        public UIChromeLegacyWindowWindow UIChromeLegacyWindowWindow
        {
            get
            {
                if ((this.mUIChromeLegacyWindowWindow == null))
                {
                    this.mUIChromeLegacyWindowWindow = new UIChromeLegacyWindowWindow(this);
                }
                return this.mUIChromeLegacyWindowWindow;
            }
        }
        #endregion
        
        #region Fields
        private UIItemGroup mUIItemGroup;
        
        private UIChromeLegacyWindowWindow mUIChromeLegacyWindowWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class UIItemGroup : WinGroup
    {
        
        public UIItemGroup(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.WindowTitles.Add("New Tab - Google Chrome");
            #endregion
        }
        
        #region Properties
        public WinEdit UIAddressandsearchbarEdit
        {
            get
            {
                if ((this.mUIAddressandsearchbarEdit == null))
                {
                    this.mUIAddressandsearchbarEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIAddressandsearchbarEdit.SearchProperties[WinEdit.PropertyNames.Name] = "Address and search bar";
                    this.mUIAddressandsearchbarEdit.WindowTitles.Add("New Tab - Google Chrome");
                    #endregion
                }
                return this.mUIAddressandsearchbarEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUIAddressandsearchbarEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class UIChromeLegacyWindowWindow : WinWindow
    {
        
        public UIChromeLegacyWindowWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlId] = "166081536";
            this.WindowTitles.Add("Documents Index - My ASP.NET Application - Google Chrome");
            #endregion
        }
        
        #region Properties
        public WinClient UIChromeLegacyWindowClient
        {
            get
            {
                if ((this.mUIChromeLegacyWindowClient == null))
                {
                    this.mUIChromeLegacyWindowClient = new WinClient(this);
                    #region Search Criteria
                    this.mUIChromeLegacyWindowClient.SearchProperties[WinControl.PropertyNames.Name] = "Chrome Legacy Window";
                    this.mUIChromeLegacyWindowClient.WindowTitles.Add("Documents Index - My ASP.NET Application - Google Chrome");
                    #endregion
                }
                return this.mUIChromeLegacyWindowClient;
            }
        }
        #endregion
        
        #region Fields
        private WinClient mUIChromeLegacyWindowClient;
        #endregion
    }
}
