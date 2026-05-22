{==============================================================================

  ThemeEngine's ComboBox
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemecomboboxs.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemecomboboxs;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Classes, Messages, Graphics, Controls, Forms, StdCtrls, ExtCtrls,
  te_controls, KsThemeListBoxs, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeComboBox class }

  TTeThemeComboBox = class(TTeCustomComboBox)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    procedure SetThemeEngine(const Value: TTeThemeEngine); virtual;
    { Edit }
    function CreatePopupMenu(AOwner: TComponent): TTeCustomPopupMenu; override;
    function CreatePopupMenuItem(AOwner: TComponent): TTeCustomItem; override;
    procedure PaintBorder; override;
    procedure PaintBackground(Rect: TRect; Canvas: TCanvas); override;
    { Combo }
    procedure BeforeDropDown; override;
    function CreateListBox(AOwner: TComponent): TTeCustomListBox; override;
    function GetButtonHeight: integer; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
  published
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
  end;

implementation {===============================================================}

uses KsThemeMenus, KsThemeItems;

{ TTeThemeComboBox }

constructor TTeThemeComboBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeComboBox.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeComboBox.Loaded;
begin
  inherited;
end;

function TTeThemeComboBox.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ Combo }

procedure TTeThemeComboBox.BeforeDropDown;
begin
  inherited;
  if ListBox <> nil then
    (ListBox as TTeThemeListBox).ThemeEngine := ThemeEngine;
end;

function TTeThemeComboBox.CreateListBox(AOwner: TComponent): TTeCustomListBox;
begin
  Result := TTeThemeListBox.Create(AOwner);
end;

function TTeThemeComboBox.GetButtonHeight: integer;
begin
  if UseTheme then
    Result := FThemeEngine.Theme.GetMetrix(ngmComboButtonWidth)
  else
    Result := inherited GetButtonHeight;
end;

{ Drawing }

procedure TTeThemeComboBox.PaintBackground(Rect: TRect; Canvas: TCanvas);
var
  R: TRect;
  State: TTeThemeButtonState;
begin
  if not UseTheme then
  begin
    inherited ;
    Exit;
  end;

  R := Rect;
  if ButtonAlign = cbaLeft then
    R.Left := R.Left - BorderWidth - 1
  else
    R.Right := R.Right + BorderWidth + 1;
  FillRect(Canvas, R, FThemeEngine.Theme.GetColor(ngcWindow));

  { Draw Button }
  R := GetButtonRect;

  if not Enabled then
    State := ngsDisabled
  else
    if ButtonPressed then
      State := ngsPressed
    else
      if MouseOnButton then
        State := ngsHot
      else
        State := ngsNormal;
        
  FThemeEngine.Theme.DrawComboButton(Canvas, R, State);

  { Paint Item }
  PaintListItem(Rect, Canvas);
end;

procedure TTeThemeComboBox.PaintBorder;
var
  R: TRect;
  State: TTeThemeButtonState;
begin
  if not UseTheme then
  begin
    inherited ;
    Exit;
  end;

  { Draw Border }
  R := Rect(0, 0, FWidth, FHeight);

  if not Enabled then
    State := ngsDisabled
  else
    State := ngsNormal;

  FThemeEngine.Theme.DrawControlFrame(Canvas, Classes.Rect(0, 0, FWidth, FHeight), State);
end;

procedure TTeThemeComboBox.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeComboBox.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeComboBox.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  if ListBox <> nil then
    (ListBox as TTeThemeListBox).ThemeEngine := ThemeEngine;
  Invalidate;
end;

procedure TTeThemeComboBox.SetVersion(const Value: TTeThemeVersion);
begin
end;

function TTeThemeComboBox.CreatePopupMenu(AOwner: TComponent): TTeCustomPopupMenu;
begin
  Result := TTeThemePopupMenu.Create(AOwner);
  (Result as TTeThemePopupMenu).ThemeEngine := FThemeEngine;
end;

function TTeThemeComboBox.CreatePopupMenuItem(AOwner: TComponent): TTeCustomItem;
begin
  Result := TTeThemeItem.Create(AOwner);
  (Result as TTeThemeItem).ThemeEngine := FThemeEngine;
end;

end.
