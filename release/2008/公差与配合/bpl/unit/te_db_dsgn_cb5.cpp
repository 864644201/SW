//---------------------------------------------------------------------------
#include <vcl.h>
#pragma hdrstop
USERES("te_db_dsgn_cb5.res");
USEUNIT("KsThemeDBReg.pas");
USERES("KsThemeDBReg.dcr");
USEUNIT("KsThemeDBControls.pas");
USEUNIT("KsThemeDBGrids.pas");
USEPACKAGE("vcl50.bpi");
USEPACKAGE("vclx50.bpi");
USEPACKAGE("bcbsmp50.bpi");
USEPACKAGE("vcldb50.bpi");
USEPACKAGE("vclbde50.bpi");
USEPACKAGE("te_dsgn_cb5.bpi");
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
