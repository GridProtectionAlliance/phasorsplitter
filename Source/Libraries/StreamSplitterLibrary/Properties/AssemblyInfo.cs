﻿using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Stream Splitter Library")]
[assembly: AssemblyDescription("Synchrophasor Stream Splitter Proxy Management Library")]
[assembly: AssemblyProduct("Synchrophasor Stream Splitter")]
[assembly: AssemblyCompany("Grid Protection Alliance")]
[assembly: AssemblyCopyright("Copyright © GPA, 2016.  All Rights Reserved.")]
[assembly: InternalsVisibleTo("StreamSplitterManager")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug Build")]
#else
[assembly: AssemblyConfiguration("Release Build")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("059e3fec-2050-44f4-b62e-dd703a5e0f37")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.1.16.0")]
[assembly: AssemblyVersion("1.1.16.0")]
[assembly: AssemblyFileVersion("1.1.16.0")]
