﻿#pragma checksum "..\..\ucExamination.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B683DF794E35AC18890B45576E7CDBEEE4175A75CD005F05FB6B907EB3102140"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Web.WebView2.Wpf;
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


namespace trustid {
    
    
    /// <summary>
    /// ucExamination
    /// </summary>
    public partial class ucExamination : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\ucExamination.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbCameraDevices;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\ucExamination.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border webcamContainer;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\ucExamination.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image webcamPreview;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\ucExamination.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnImpersonation;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\ucExamination.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnForbiddenApp;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\ucExamination.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFeedback;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\ucExamination.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSendUserFeedback;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/trustid;component/ucexamination.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ucExamination.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cmbCameraDevices = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.webcamContainer = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.webcamPreview = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.btnImpersonation = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.btnForbiddenApp = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.txtFeedback = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnSendUserFeedback = ((System.Windows.Controls.Button)(target));
            
            #line 127 "..\..\ucExamination.xaml"
            this.btnSendUserFeedback.Click += new System.Windows.RoutedEventHandler(this.btnSendUserFeedback_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

