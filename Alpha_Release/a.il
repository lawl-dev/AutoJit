
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.0.30319.18020
//  Copyright (c) Microsoft Corporation. Alle Rechte vorbehalten.



// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern AutoJIT.Runtime
{
  .ver 1:0:0:0
}
.assembly AutoJITAssembly
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.
  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module AutoJITAssembly.exe
// MVID: {08095FA8-8BE0-4ED5-AF10-B3FBEBAD9B23}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000003    //  ILONLY 32BITREQUIRED
// Image base: 0x06C20000


// =============== CLASS MEMBERS DECLARATION ===================

.class public auto ansi beforefieldinit AutoJITScript.AutoJITScriptClass
       extends [mscorlib]System.Object
{
  .field private class [AutoJIT.Runtime]AutoJITRuntime.AutoitContext`1<class AutoJITScript.AutoJITScriptClass> _context
  .field private class [AutoJIT.Runtime]AutoJITRuntime.AutoitRuntime`1<class AutoJITScript.AutoJITScriptClass> _functions
  .field private class [AutoJIT.Runtime]AutoJITRuntime.Variant v_i
  .method public hidebysig instance class [AutoJIT.Runtime]AutoJITRuntime.Variant 
          Main() cil managed
  {
    // Code size       129 (0x81)
    .maxstack  3
    IL_0000:  ldarg.0
    IL_0001:  ldarg.0
    IL_0002:  call       instance class [AutoJIT.Runtime]AutoJITRuntime.Variant AutoJITScript.AutoJITScriptClass::f_GetValue()
    IL_0007:  stfld      class [AutoJIT.Runtime]AutoJITRuntime.Variant AutoJITScript.AutoJITScriptClass::v_i
    IL_000c:  ldarg.0
    IL_000d:  ldfld      class [AutoJIT.Runtime]AutoJITRuntime.AutoitRuntime`1<class AutoJITScript.AutoJITScriptClass> AutoJITScript.AutoJITScriptClass::_functions
    IL_0012:  ldarg.0
    IL_0013:  ldfld      class [AutoJIT.Runtime]AutoJITRuntime.Variant AutoJITScript.AutoJITScriptClass::v_i
    IL_0018:  ldc.i4.1
    IL_0019:  call       class [AutoJIT.Runtime]AutoJITRuntime.Variant [AutoJIT.Runtime]AutoJITRuntime.Variant::Create(int32)
    IL_001e:  callvirt   instance class [AutoJIT.Runtime]AutoJITRuntime.Variant class [AutoJIT.Runtime]AutoJITRuntime.AutoitRuntime`1<class AutoJITScript.AutoJITScriptClass>::Equal(class [AutoJIT.Runtime]AutoJITRuntime.Variant,
                                                                                                                                                                                     class [AutoJIT.Runtime]AutoJITRuntime.Variant)
    IL_0023:  call       bool [AutoJIT.Runtime]AutoJITRuntime.Variant::op_Implicit(class [AutoJIT.Runtime]AutoJITRuntime.Variant)
    IL_0028:  brfalse.s  IL_0031

    IL_002a:  nop
    IL_002b:  nop
    IL_002c:  br.s       IL_004f

    IL_002e:  nop
    IL_002f:  br.s       IL_007a

    IL_0031:  ldarg.0
    IL_0032:  ldfld      class [AutoJIT.Runtime]AutoJITRuntime.AutoitRuntime`1<class AutoJITScript.AutoJITScriptClass> AutoJITScript.AutoJITScriptClass::_functions
    IL_0037:  ldarg.0
    IL_0038:  ldfld      class [AutoJIT.Runtime]AutoJITRuntime.Variant AutoJITScript.AutoJITScriptClass::v_i
    IL_003d:  ldc.i4.2
    IL_003e:  call       class [AutoJIT.Runtime]AutoJITRuntime.Variant [AutoJIT.Runtime]AutoJITRuntime.Variant::Create(int32)
    IL_0043:  callvirt   instance class [AutoJIT.Runtime]AutoJITRuntime.Variant class [AutoJIT.Runtime]AutoJITRuntime.AutoitRuntime`1<class AutoJITScript.AutoJITScriptClass>::Equal(class [AutoJIT.Runtime]AutoJITRuntime.Variant,
                                                                                                                                                                                     class [AutoJIT.Runtime]AutoJITRuntime.Variant)
    IL_0048:  call       bool [AutoJIT.Runtime]AutoJITRuntime.Variant::op_Implicit(class [AutoJIT.Runtime]AutoJITRuntime.Variant)
    IL_004d:  brfalse.s  IL_0056

    IL_004f:  nop
    IL_0050:  nop
    IL_0051:  br.s       IL_0074

    IL_0053:  nop
    IL_0054:  br.s       IL_007a

    IL_0056:  ldarg.0
    IL_0057:  ldfld      class [AutoJIT.Runtime]AutoJITRuntime.AutoitRuntime`1<class AutoJITScript.AutoJITScriptClass> AutoJITScript.AutoJITScriptClass::_functions
    IL_005c:  ldarg.0
    IL_005d:  ldfld      class [AutoJIT.Runtime]AutoJITRuntime.Variant AutoJITScript.AutoJITScriptClass::v_i
    IL_0062:  ldc.i4.3
    IL_0063:  call       class [AutoJIT.Runtime]AutoJITRuntime.Variant [AutoJIT.Runtime]AutoJITRuntime.Variant::Create(int32)
    IL_0068:  callvirt   instance class [AutoJIT.Runtime]AutoJITRuntime.Variant class [AutoJIT.Runtime]AutoJITRuntime.AutoitRuntime`1<class AutoJITScript.AutoJITScriptClass>::Equal(class [AutoJIT.Runtime]AutoJITRuntime.Variant,
                                                                                                                                                                                     class [AutoJIT.Runtime]AutoJITRuntime.Variant)
    IL_006d:  call       bool [AutoJIT.Runtime]AutoJITRuntime.Variant::op_Implicit(class [AutoJIT.Runtime]AutoJITRuntime.Variant)
    IL_0072:  brfalse.s  IL_0078

    IL_0074:  nop
    IL_0075:  nop
    IL_0076:  br.s       IL_007a

    IL_0078:  nop
    IL_0079:  nop
    IL_007a:  ldnull
    IL_007b:  call       class [AutoJIT.Runtime]AutoJITRuntime.Variant [AutoJIT.Runtime]AutoJITRuntime.Variant::Create(object)
    IL_0080:  ret
  } // end of method AutoJITScriptClass::Main

  .method public hidebysig instance class [AutoJIT.Runtime]AutoJITRuntime.Variant 
          f_GetValue() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldc.i4.1
    IL_0001:  call       class [AutoJIT.Runtime]AutoJITRuntime.Variant [AutoJIT.Runtime]AutoJITRuntime.Variant::Create(int32)
    IL_0006:  ret
  } // end of method AutoJITScriptClass::f_GetValue

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       55 (0x37)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldnull
    IL_0002:  call       class [AutoJIT.Runtime]AutoJITRuntime.Variant [AutoJIT.Runtime]AutoJITRuntime.Variant::Create(object)
    IL_0007:  stfld      class [AutoJIT.Runtime]AutoJITRuntime.Variant AutoJITScript.AutoJITScriptClass::v_i
    IL_000c:  ldarg.0
    IL_000d:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0012:  ldarg.0
    IL_0013:  ldarg.0
    IL_0014:  newobj     instance void class [AutoJIT.Runtime]AutoJITRuntime.AutoitContext`1<class AutoJITScript.AutoJITScriptClass>::.ctor(!0)
    IL_0019:  stfld      class [AutoJIT.Runtime]AutoJITRuntime.AutoitContext`1<class AutoJITScript.AutoJITScriptClass> AutoJITScript.AutoJITScriptClass::_context
    IL_001e:  ldarg.0
    IL_001f:  ldarg.0
    IL_0020:  ldfld      class [AutoJIT.Runtime]AutoJITRuntime.AutoitContext`1<class AutoJITScript.AutoJITScriptClass> AutoJITScript.AutoJITScriptClass::_context
    IL_0025:  newobj     instance void class [AutoJIT.Runtime]AutoJITRuntime.AutoitRuntime`1<class AutoJITScript.AutoJITScriptClass>::.ctor(class [AutoJIT.Runtime]AutoJITRuntime.AutoitContext`1<!0>)
    IL_002a:  stfld      class [AutoJIT.Runtime]AutoJITRuntime.AutoitRuntime`1<class AutoJITScript.AutoJITScriptClass> AutoJITScript.AutoJITScriptClass::_functions
    IL_002f:  ldarg.0
    IL_0030:  call       instance class [AutoJIT.Runtime]AutoJITRuntime.Variant AutoJITScript.AutoJITScriptClass::Main()
    IL_0035:  pop
    IL_0036:  ret
  } // end of method AutoJITScriptClass::.ctor

} // end of class AutoJITScript.AutoJITScriptClass

.class private auto ansi beforefieldinit AutoJITScript.Program
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  newobj     instance void AutoJITScript.AutoJITScriptClass::.ctor()
    IL_0005:  pop
    IL_0006:  ret
  } // end of method Program::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method Program::.ctor

} // end of class AutoJITScript.Program


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
