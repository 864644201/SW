//---------------------------------------------------------------------------
#include <vcl.h>
#pragma hdrstop
USERES("te_db_dsgn_cb4.res");
USEPACKAGE("vcl40.bpi");
USEPACKAGE("Vclx40.bpi");
USEPACKAGE("bcbsmp40.bpi");
USEUNIT("KsThemeDBReg.pas");
USERES("KsThemeDBReg.dcr");
USEUNIT("KsThemeDBControls.pas");
USEUNIT("KsThemeDBGrids.pas");
USEPACKAGE("Vcldb40.bpi");
USEPACKAGE("te_dsgn_cb4.bpi");
//---------------------------------------------------------------------------
#pragma package(smart_init)
//---------------------------------------------------------------------------
//   Package source.
//---------------------------------------------------------------------------
int WINAPI DllEntryPoint(HINSTANCE hinst, unsigned long reason, void*)
{
        return 1;
}
//---------------------------------------------------------------------------
