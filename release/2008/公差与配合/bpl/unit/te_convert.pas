{==============================================================================

  ThemeEngine Converter
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: te_convert.pas,v 1.4 2002/10/28 21:04:01 Evgeny Exp $

===============================================================================}

unit te_convert;

{$I te_define.inc}

interface

uses
  Windows, SysUtils, Classes, Controls, Forms, Menus, StdCtrls,
  te_controls, te_designer, ksthemeengine;

type

  TTeThemeConverter = class(TTeCustomConverter)
  private
    FThemeEngine: TTeThemeEngine;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
  public
    constructor Create(AOwner: TComponent); override;
    function GetClass(ClassKind: TTeClassKind): TComponentClass; override;
    procedure SetAdvancedProp(AOldObject, ANewObject: TObject); override;

    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
  end;

  TTeThemeConverterClass = class of TTeThemeConverter;

implementation {===============================================================}

uses TypInfo, ksthemebuttons, ksthemeedits, ksthemegroupboxs, ksthemecheckboxs,
  ksthemetrackbars, ksthemeprogress, ksthemepanels, ksthemescrollbars, ksthemetabs,
  ksthemelistboxs, ksthemecomboboxs, ksthemespeedbuttons, ksthemespinbuttons,
  ksthemeforms, ksthemestdcontrol, ksthemespinedit, ksthemetoolbars,
  ksthemecontrolbars, ksthemesplitter, ksthemegrids, ksthemelabels,
  ksthemestatusbar, ksthemeheader;

{ TTeThemeConverter }

constructor TTeThemeConverter.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

{ Virtual }

function TTeThemeConverter.GetClass(ClassKind: TTeClassKind): TComponentClass;
begin
  case ClassKind of
    ckCheckBox: Result := TTeThemeCheckBox;
    ckRadioButton: Result := TTeThemeRadioButton;
    ckTrackBar: Result := TTeThemeTrackBar;
    ckGroupBox: Result := TTeThemeGroupBox;
    ckRadioGroup: Result := TTeThemeRadioGroup;
    ckScrollBar: Result := TTeThemeScrollBar;
    ckListBox: Result := TTeThemeListBox;
    ckEdit: Result := TTeThemeEdit;
    ckComboBox: Result := TTeThemeComboBox;
    ckSpeedButton: Result := TTeThemeSpeedButton;
    ckSpinButton: Result := TTeThemeSpinButton;
    ckButton: Result := TTeThemeButton;
    ckProgressBar: Result := TTeThemeProgressBar;
    ckTabControl: Result := TTeThemeTabControl;
    ckCustomForm: Result := TTeThemeForm;
    ckEngine: Result := TTeThemeEngine;
    ckMemo: Result := TTeThemeMemo;
    ckSpinEdit: Result := TTeThemeSpinEdit;
    ckSplitter: Result := TTeThemeSplitter;
    ckMaskEdit: Result := TTeThemeMaskEdit;
    ckScrollBox: Result := TTeThemeScrollBox;
    ckLabel: Result := TTeThemeLabel;
    ckStringGrid: Result := TTeThemeStringGrid;
    ckDrawGrid: Result := TTeThemeDrawGrid;
    ckToolbar: Result := TTeThemeToolbar;
    ckControlBar: Result := TTeThemeControlBar;
    ckStatusBar: Result := TTeThemeStatusBar;
    ckHeaderControl: Result := TTeThemeHeaderControl;
    ckTreeView: Result := TTeThemeSTreeView;
    ckListView: Result := TTeThemeSListView;
    ckPanel: Result := TTeThemePanel;
  else
    Result := nil;
  end;
end;

procedure TTeThemeConverter.SetAdvancedProp(AOldObject, ANewObject: TObject);
begin
  inherited SetAdvancedProp(AOldObject, ANewObject);

  { Set ThemeEngine Property }
  if ANewObject is TComponent then
    SetPropertyIfExists(TComponent(ANewObject), 'ThemeEngine', FThemeEngine);
end;

procedure TTeThemeConverter.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
end;

end.
