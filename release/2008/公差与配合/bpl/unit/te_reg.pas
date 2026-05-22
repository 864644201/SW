{==============================================================================

  CommonLib IDE Routines               
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All contents of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information
 
  $Id: te_reg.pas,v 1.10.2.1 2003/01/23 16:14:46 evgeny Exp $

===============================================================================}

unit te_reg;

interface

{$I te_define.inc}

uses
  Classes, SysUtils, {$IFDEF KS_COMPILER6_UP} DesignIntf, DesignEditors, VCLEditors {$ELSE} DsgnIntf {$ENDIF};

procedure Register;

implementation {===============================================================}

uses te_controls, te_designer;

{ Register Routines ===========================================================}

procedure Register;
begin
  RegisterClass(TTeCustomMenuBar);
  RegisterClass(TTeCustomPopupMenu);
  RegisterClass(TTeCustomPanel);
  RegisterClass(TTeCustomButton);
  RegisterClass(TTeCustomCheckBox);
  RegisterClass(TTeCustomRadioButton);
  RegisterClass(TTeCustomProgressBar);
  RegisterClass(TTeCustomTrackBar);
  RegisterClass(TTeCustomRangeBar);
  RegisterClass(TTeCustomGroupBox);
  RegisterClass(TTeCustomScrollBar);
  RegisterClass(TTeCustomTabControl);
  RegisterClass(TTeCustomHint);
  RegisterClass(TTeCustomMessage);
  RegisterClass(TTeCustomSpeedButton);
  RegisterClass(TTeCustomListBox);
  RegisterClass(TTeCustomSpinButton);
  RegisterClass(TTeCustomEdit);
  RegisterClass(TTeCustomComboBox);
  RegisterClass(TTeCustomLabel);
  RegisterClass(TTeCustomControlBar);
  RegisterClass(TTeCustomToolBar);
  RegisterClass(TTeCustomDrawGrid);
  RegisterClass(TTeCustomStringGrid); 

  RegisterNoIcon([TTeCustomTabSheet]);
  RegisterNoIcon([TTeSpeedDivider, TTeSpeedContainer]);
  RegisterNoIcon([TTeCustomItem]);

  {$IFDEF KS_COMPILER6_UP}
  { TShortCut properties show up like Integer properties in Delphi 6
    without this... }
  RegisterPropertyEditor (TypeInfo(TShortCut), TTeCustomItem, '',
    TShortCutProperty);
  {$ENDIF}
  RegisterPropertyEditor(TypeInfo(TTeCustomItem), TTeCustomForm, 'WindowMenu', TComponentProperty);
end;

end.

