{==============================================================================

  ThemeEngine's SpinButton
  Copyright (C) 2000-2002 by Evgeny Kryukov
  Copyright (C) 2002 by DKJ
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemespinedit.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit ksthemespinedit;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Classes, Messages, Graphics, Controls, Forms, StdCtrls, ExtCtrls,
  te_controls, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeSpinEdit class }

  TTeThemeSpinEdit = class(TTeCustomSpinEdit)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    { overrides }
    function CreatePopupMenu(AOwner: TComponent): TTeCustomPopupMenu; override;
    function CreatePopupMenuItem(AOwner: TComponent): TTeCustomItem; override;
    procedure PaintBorder; override;
    procedure PaintBackground(Rect: TRect; Canvas: TCanvas); override;
    { Button}
    function CreateButton(AOwner: TComponent): TTeInplaceControl; override;
    { VCL protected }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
  published
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion stored false;
  end;

implementation {===============================================================}

uses KsThemeMenus, KsThemeItems, KsThemeThemes, KsThemeSpinButtons;

{ TTeThemeSpinEdit }

constructor TTeThemeSpinEdit.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeSpinEdit.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeSpinEdit.Loaded;
begin
  inherited Loaded;
end;

function TTeThemeSpinEdit.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ Edits }

function TTeThemeSpinEdit.CreatePopupMenu(AOwner: TComponent): TTeCustomPopupMenu;
begin
  Result := TTeThemePopupMenu.Create(AOwner);
  (Result as TTeThemePopupMenu).ThemeEngine := FThemeEngine;
end;

function TTeThemeSpinEdit.CreatePopupMenuItem(AOwner: TComponent): TTeCustomItem;
begin
  Result := TTeThemeItem.Create(AOwner);
  (Result as TTeThemeItem).ThemeEngine := FThemeEngine;
end;

procedure TTeThemeSpinEdit.PaintBackground(Rect: TRect; Canvas: TCanvas);
begin
  if UseTheme then
  begin
    if ButtonAlign = baLeft then
      Rect.Left := Rect.Left - ButtonWidth
    else
      Rect.Right := Rect.Right + ButtonWidth;
    FillRect(Canvas, Rect, FThemeEngine.Theme.GetColor(ngcWindow));
  end
  else
    inherited ;
end;

procedure TTeThemeSpinEdit.PaintBorder;
var
  State: TTeThemeButtonState;
begin
  if UseTheme then
  begin
    if not Enabled then
      State := ngsDisabled
    else
      State := ngsNormal;

    FThemeEngine.Theme.DrawControlFrame(Canvas, Rect(0, 0, Width, Height), State);
  end
  else
    inherited ;
end;

procedure TTeThemeSpinEdit.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeSpinEdit.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeSpinEdit.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  if (PopupMenu <> nil) then
    (PopupMenu as TTeThemePopupMenu).ThemeEngine := Value;

  if Button<>nil then
    (Button as TTeThemeSpinButton).ThemeEngine := Value;

  Invalidate;
end;

procedure TTeThemeSpinEdit.SetVersion(const Value: TTeThemeVersion);
begin
end;

{ Button }

function TTeThemeSpinEdit.CreateButton(AOwner: TComponent): TTeInplaceControl;
begin
  Result := TTeThemeSpinButton.Create(AOwner);
  (Result as TTeThemeSpinButton).ThemeEngine := FThemeEngine;
end;

end.
