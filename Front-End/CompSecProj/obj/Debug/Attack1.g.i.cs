// Updated by XamlIntelliSenseFileGenerator 3/8/2023 11:03:36 PM
#pragma checksum "..\..\Attack1.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "15EE20A2A999A2E83F462C006E32ABD53932F0545F797DE6F8422CD484535A5F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using trustid;


namespace trustid
{


    /// <summary>
    /// ucEnrollStepTwo
    /// </summary>
    public partial class Attack1 : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {


#line 31 "..\..\Attack1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblProcesses;

#line default
#line hidden


#line 56 "..\..\Attack1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Step2;

#line default
#line hidden


#line 64 "..\..\Attack1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock IdentityVerification;

#line default
#line hidden


#line 73 "..\..\Attack1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblAlerts;

#line default
#line hidden


#line 97 "..\..\Attack1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRunMonitoring;

#line default
#line hidden


#line 128 "..\..\Attack1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextStep;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CompSec;component/attack1.xaml", System.UriKind.Relative);

#line 1 "..\..\Attack1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.lblProcesses = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 2:
                    this.Step2 = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 3:
                    this.IdentityVerification = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 4:
                    this.lblAlerts = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 5:
                    this.btnRunMonitoring = ((System.Windows.Controls.Button)(target));

#line 100 "..\..\Attack1.xaml"
                    this.btnRunMonitoring.Click += new System.Windows.RoutedEventHandler(this.btnRunMonitoring_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.btnNextStep = ((System.Windows.Controls.Button)(target));
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

