﻿#pragma checksum "..\..\MacScenarioPart_2.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "84274A77BF273ADD49B5C5850A9714809205CC7A00F5A3CF52B0EE44A2346EDB"
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


namespace trustid {
    
    
    /// <summary>
    /// MacScenarioPart_2
    /// </summary>
    public partial class MacScenarioPart_2 : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 86 "..\..\MacScenarioPart_2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MacroOne;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\MacScenarioPart_2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FileOpen;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\MacScenarioPart_2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock attacker;
        
        #line default
        #line hidden
        
        
        #line 200 "..\..\MacScenarioPart_2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock attacker_Copy;
        
        #line default
        #line hidden
        
        
        #line 218 "..\..\MacScenarioPart_2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MacroResults;
        
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
            System.Uri resourceLocater = new System.Uri("/CompSec;component/macscenariopart_2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MacScenarioPart_2.xaml"
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
            this.MacroOne = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\MacScenarioPart_2.xaml"
            this.MacroOne.Click += new System.Windows.RoutedEventHandler(this.MacroOne_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.FileOpen = ((System.Windows.Controls.Button)(target));
            
            #line 152 "..\..\MacScenarioPart_2.xaml"
            this.FileOpen.Click += new System.Windows.RoutedEventHandler(this.FileOpen_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.attacker = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.attacker_Copy = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.MacroResults = ((System.Windows.Controls.Button)(target));
            
            #line 221 "..\..\MacScenarioPart_2.xaml"
            this.MacroResults.Click += new System.Windows.RoutedEventHandler(this.MacroResults_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

