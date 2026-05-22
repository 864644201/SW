{==============================================================================

  ThemeEngine's Switcher
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemeswitch.pas,v 1.2 2002/10/28 21:04:00 Evgeny Exp $

===============================================================================}

unit ksthemeswitch;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses Windows, Forms, Messages, Sysutils, Classes, Graphics, Controls, 
  te_controls, KsThemeThemes, KsThemeEngine, KsThemeComboBoxs, KsThemeVersion;

type

{ TTeSwitcherComboBox class }

  TTeSwitcherComboBox = class(TTeThemeComboBox)
  private
    FChanging: boolean;
  protected
    procedure BuildThemeList;

    procedure SetThemeEngine(const Value: TTeThemeEngine); override;
    procedure SetParent(AParent: TWinControl); override;

    procedure Change; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;

    property Items;
  published
  end;

implementation {===============================================================}

{ TTeSwitcherComboBox }

constructor TTeSwitcherComboBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);

  ComboStyle := kcsDropDownList;
end;

destructor TTeSwitcherComboBox.Destroy;
begin
  inherited Destroy;
end;

procedure TTeSwitcherComboBox.Loaded;
begin
  inherited Loaded;
  ComboStyle := kcsDropDownList;

  BuildThemeList;
end;

procedure TTeSwitcherComboBox.SetParent(AParent: TWinControl);
begin
  inherited;

  if AParent <> nil then
    BuildThemeList;
end;

procedure TTeSwitcherComboBox.BuildThemeList;
var
  i: integer;
  ATheme: TTeThemeClass;
begin
  Items.Clear;

  if csDesigning in ComponentState then
  begin
    Items.Add('<Theme''s Switcher>');
    ItemIndex := 0;
  end
  else
  begin
    for i := 0 to ThemesList.Count - 1 do
    begin
      ATheme := TTeThemeClass(ThemesList[i]);

      Items.AddObject(ATheme.GetThemeName, TObject(ATheme));

      if (ThemeEngine <> nil) and (ThemeEngine.Theme.ClassName = ATheme.ClassName) and
         (ItemIndex <> i) then
      begin
        ItemIndex := i;
        Change;
      end;
    end;
  end;
end;

procedure TTeSwitcherComboBox.Change;
var
  ATheme: TTeThemeClass;
begin
  inherited Change;

  if (ItemIndex < 0) then Exit;
  if FChanging then Exit;

  FChanging := true;
  try
    if not (csDesigning in ComponentState) then
    begin
      ATheme := TTeThemeClass(Items.Objects[ItemIndex]);

      if (ATheme <> nil) and (ThemeEngine <> nil) then
      begin
        if not (csLoading in ComponentState) and (ThemeEngine.Theme.ClassName <> ATheme.ClassName) then
          ThemeEngine.ChangeTheme(ATheme);
      end;
    end;
  finally
    FChanging := false;
  end;
end;

procedure TTeSwitcherComboBox.SetThemeEngine(const Value: TTeThemeEngine);
begin
  inherited;
  BuildThemeList;
end;

end.




