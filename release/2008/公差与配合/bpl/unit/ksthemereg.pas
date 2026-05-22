{==============================================================================

  Flat Theme
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemereg.pas,v 1.2 2002/08/11 23:09:24 Evgeny Exp $

===============================================================================}

unit ksthemereg;

{$I te_define.inc}

interface

uses
  Classes, SysUtils, Forms, Controls, Dialogs, {$IFDEF KS_COMPILER6_UP} DesignIntf, DesignEditors {$ELSE} DsgnIntf {$ENDIF};

procedure Register;

implementation {===============================================================}

uses te_controls, te_designer, te_convert, ksthemeversion,
  ksthemethemes, ksthemeengine, ksthemeforms, ksthemeitems, 
  KsThemeMenus, KsThemeButtons, KsThemeCheckBoxs, KsThemeTrackBars,
  KsThemeProgress, KsThemePanels, KsThemeGroupBoxs, KsThemeScrollBars,
  KsThemeTabs, KsThemeEdits, KsThemeHints, KsThemeMessages, KsThemeListBoxs,
  KsThemeComboBoxs, KsThemeSpeedButtons, KsThemeSpinButtons,
  KsThemeControlBars, KsThemeToolBars, KsThemeLabels, KsThemeSplitter,
  KsThemeHeader, KsThemeStatusBar, KsThemeStdControl,
  KsThemeSwitch, KsThemeGrids, KsThemeInplaceEdit, KsThemeSpinEdit;

{ IDE Editors }

type

  TTeThemePropertyEditor = class(TClassProperty)
  public
    function GetAttributes: TPropertyAttributes; override;
    function GetValue: string; override;
    procedure GetValues(Proc: TGetStrProc); override;
    procedure SetValue(const Value: string); override;
  end;

{ TTeThemePropertyEditor }

function TTeThemePropertyEditor.GetAttributes: TPropertyAttributes;
begin
  Result := [paValueList];
end;

function TTeThemePropertyEditor.GetValue: string;
begin
  if (GetComponent(0) <> nil) and (TTeThemeEngine(GetComponent(0)).Theme <> nil) then
    Result := TTeThemeEngine(GetComponent(0)).Theme.GetThemeName
  else
    Result := '(none)';
end;

procedure TTeThemePropertyEditor.GetValues(Proc: TGetStrProc);
var
  i: integer;
begin
  try
    for i := 0 to ThemesList.Count-1 do
      Proc(TTeThemeClass(ThemesList[i]).GetThemeName);
  except
    on E: Exception do MessageDlg(E.Message, mtError, [mbOk], 0);
  end;
end;

procedure TTeThemePropertyEditor.SetValue(const Value: string);
var
  i: integer;
begin
  try
    for i := 0 to ThemesList.Count-1 do
      if TTeThemeClass(ThemesList[i]).GetThemeName = Value then
        if (GetComponent(0) <> nil) then
        begin
          TTeThemeEngine(GetComponent(0)).ChangeTheme(TTeThemeClass(TTeThemeClass(ThemesList[i])));

          Designer.Modified;
          
          Break;
        end;
  except
    on E: Exception do MessageDlg(E.Message, mtError, [mbOk], 0);
  end;
end;

{ Converter }

type

  TTeThemeCompConverter = class(TDefaultEditor)
  public
    procedure Edit; override;
    procedure ExecuteVerb (Index: integer); override;
    function GetVerb (Index: integer): String; override;
    function GetVerbCount: integer; override;
  end;

procedure TTeThemeCompConverter.Edit;
var
  C: TfrmConvertForm;
  ThemeEngine: TTeThemeEngine;
begin
  if Assigned(Component) then
  begin
    C := TfrmConvertForm.CreateForm;
    try
      { Initialization }
      C.ConvertClass := TTeThemeConverter;
      C.ConvertForm := TCustomForm(Component.Owner);
      C.ConvertName := 'ThemeEngine';
      C.InitConverter;
      { Set ThemeEngine }
      ThemeEngine := nil;
      if Component is TTeThemeEngine then
        ThemeEngine := TTeThemeEngine(Component);
      if Component is TTeThemeForm then
        ThemeEngine := TTeThemeForm(Component).ThemeEngine;

      if (ThemeEngine <> nil) and (C.Converter <> nil) and (C.Converter is TTeThemeConverter) then
        TTeThemeConverter(C.Converter).ThemeEngine := ThemeEngine;
      { Show }
      C.ShowModal;
    finally
      { Free }
      C.Free;
    end;
  end;
end;

procedure TTeThemeCompConverter.ExecuteVerb (Index: integer);
begin
  if Index = 0 then
    Edit;
end;

function TTeThemeCompConverter.GetVerbCount: integer;
begin
  Result := 1;
end;

function TTeThemeCompConverter.GetVerb(Index: integer): String;
begin
  if Index = 0 then
    Result := 'Component Converter...'
  else
    Result := '';
end; 

type

{ TTeVersionPropertyEditor }

  TTeVersionPropertyEditor = class(TPropertyEditor)
  public
    procedure Edit; override;
    function GetAttributes: TPropertyAttributes; override;
    function GetValue: String; override;
  end;

{ TTeVersionPropertyEditor ====================================================}

procedure TTeVersionPropertyEditor.Edit;
begin
  ShowVersion;
end;

function TTeVersionPropertyEditor.GetAttributes: TPropertyAttributes;
begin
  Result := inherited GetAttributes + [paDialog, paReadOnly];
end;

function TTeVersionPropertyEditor.GetValue: String;
begin
  Result := 'About ' + sTeThemeVersionPropText;
end;

{ Toolbar editor }

type

  TTeThemeToolbarEditor = class(TComponentEditor)
  public
    function GetVerbCount: Integer; override;
    function GetVerb( Index: Integer ): string; override;
    procedure ExecuteVerb( Index: Integer ); override;
  end;

function TTeThemeToolbarEditor.GetVerbCount: Integer;
begin
  Result := 3;
end;

function TTeThemeToolbarEditor.GetVerb(Index: Integer): string;
begin
  with Component as TTeThemeToolbar do
  begin
    case Index of
      0: Result := 'Add Button';
      1: Result := 'Add Divider';
      2: Result := 'Add Container';
    end; { case }
  end;
end;

procedure TTeThemeToolbarEditor.ExecuteVerb(Index: Integer);
var
  btnButton: TTeThemeSpeedButton;
  btnDiv: TTeSpeedDivider;
  btnCont: TTeSpeedContainer;
begin
  case Index of
    0: { Speedbutton }
    begin
      btnButton := TTeThemeSpeedButton.Create(Designer.GetRoot);
      btnButton.Name := Designer.UniqueName(btnButton.ClassName);
      btnButton.Parent := TWinControl(Component);
      btnButton.Left := 1000;
      btnButton.Top := 1000;

      Designer.SelectComponent(btnButton);
    end;
    1: { Divider }
    begin
      btnDiv := TTeSpeedDivider.Create(Designer.GetRoot);
      btnDiv.Name := Designer.UniqueName(btnDiv.ClassName);
      btnDiv.Parent := TWinControl(Component);
      btnDiv.Left := 1000;
      btnDiv.Top := 1000;

      Designer.SelectComponent(btnDiv);
    end;
    2: { Container }
    begin
      btnCont := TTeSpeedContainer.Create(Designer.GetRoot);
      btnCont.Name := Designer.UniqueName(btnCont.ClassName);
      btnCont.Parent := TWinControl(Component);
      btnCont.Left := 1000;
      btnCont.Top := 1000;

      Designer.SelectComponent(btnCont);
    end;
  end;

  if Component is TTeThemeToolbar then
    TTeThemeToolbar(Component).Realign;

  Designer.Modified;
end;

{ Register classes ============================================================}

procedure Register;
begin
  RegisterComponents('ThemeEngine', [TTeThemeEngine, TTeSwitcherComboBox,
    TTeThemeForm, TTeThemeMenuBar, TTeThemePopupMenu, TTeThemeButton,
    TTeThemeLabel, TTeThemeCheckBox, TTeThemeRadioButton, TTeThemeTrackBar,
    TTeThemeProgressBar, TTeThemePanel, TTeThemeGroupBox, TTeThemeRadioGroup,
    TTeThemeScrollBar, TTeThemeTabControl, TTeThemePageControl, TTeThemeListBox,
    TTeThemeEdit, TTeThemeComboBox, TTeThemeHint, TTeThemeMessage,
    TTeThemeSpeedButton, TTeThemeSpinButton, TTeThemeControlBar, TTeThemeSplitter,
    TTeThemeToolBar, TTeThemeHeaderControl, TTeThemeStatusBar,
    TTeThemeScrollBox, TTeThemeMaskEdit, TTeThemeMemo,
    TTeThemeDrawGrid, TTeThemeStringGrid, TTeThemeSpinEdit,
    TTeThemeEditButton, TTeThemeSTreeView, TTeThemeSListView]);

  RegisterNoIcon([TTeThemeItem]);

  { Tabs }
  RegisterComponentEditor(TTeCustomTabControl, TTeTabControlEditor);
  RegisterComponentEditor(TTeCustomTabSheet, TTeTabControlEditor);
  { Converter }
  RegisterComponentEditor(TTeThemeForm, TTeThemeCompConverter);
  RegisterComponentEditor(TTeThemeEngine, TTeThemeCompConverter);
  { Items }
  RegisterComponentEditor(TTeCustomPopupMenu, TTePopupMenuEditor);
  RegisterComponentEditor(TTeCustomMenuBar, TTeMenuBarEditor);
  RegisterPropertyEditor(TypeInfo(TTeCustomItem), nil, 'Items', TTeItemsPropertyEditor);
  RegisterPropertyEditor(TypeInfo(TTeThemeItem), nil, 'Items', TTeItemsPropertyEditor);
  { Toolbar Editors }
  RegisterComponentEditor(TTeThemeToolbar, TTeThemeToolbarEditor);
  { Theme Property }
  RegisterPropertyEditor(TypeInfo(TTeTheme), TTeThemeEngine, 'Theme', TTeThemePropertyEditor);
  { Version }
  RegisterPropertyEditor(TypeInfo(TTeThemeVersion), nil, '', TTeVersionPropertyEditor);
end;

end.

