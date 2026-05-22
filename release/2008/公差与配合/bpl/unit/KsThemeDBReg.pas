{==============================================================================

  ThemeEngine's DB Registration
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: KsThemeDBReg.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemedbreg;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Classes, SysUtils, Forms, Dialogs, {$IFDEF KS_COMPILER6_UP} DesignIntf, DesignEditors {$ELSE} DsgnIntf {$ENDIF};

procedure Register;

implementation {===============================================================}

uses KsThemeDBControls, KsThemeDBGrids;

{ Register classes ============================================================}

procedure Register;
begin
  RegisterComponents('ThemeEngine DB', [
    TTeThemeDBText,
    TTeThemeDBEdit,
    TTeThemeDBMaskEdit,
    TTeThemeDBMemo,
    TTeThemeDBComboBox,
    TTeThemeDBCheckBox,
    TTeThemeDBListBox,
    TTeThemeDBRadioGroup,
    TTeThemeDBGrid,
    TTeThemeDBLookupListBox,
    TTeThemeDBLookupComboBox,
    TTeThemeDBEditButton
    ]);
end;

end.

