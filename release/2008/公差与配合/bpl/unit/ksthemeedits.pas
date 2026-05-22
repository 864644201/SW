{==============================================================================

  ThemeEngine's Button
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemeedits.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemeedits;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Classes, Messages, Graphics, Controls, Forms, StdCtrls, ExtCtrls,
  te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeEdit class }

  TTeThemeEdit = class(TTeCustomEdit)
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
    { VCL protected }
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

{ TTeThemeEdit }

constructor TTeThemeEdit.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeEdit.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeEdit.Loaded;
begin
  inherited Loaded;
end;

function TTeThemeEdit.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ Edits }

function TTeThemeEdit.CreatePopupMenu(AOwner: TComponent): TTeCustomPopupMenu;
begin
  Result := TTeThemePopupMenu.Create(AOwner);
  (Result as TTeThemePopupMenu).ThemeEngine := FThemeEngine;
end;

function TTeThemeEdit.CreatePopupMenuItem(AOwner: TComponent): TTeCustomItem;
begin
  Result := TTeThemeItem.Create(AOwner);
  (Result as TTeThemeItem).ThemeEngine := FThemeEngine;
end;

procedure TTeThemeEdit.PaintBackground(Rect: TRect; Canvas: TCanvas);
begin
  if UseTheme then
  begin
    FillRect(Canvas, Rect, FThemeEngine.Theme.GetColor(ngcWindow));
  end
  else
    inherited ;
end;

procedure TTeThemeEdit.PaintBorder;
var
  State: TTeThemeButtonState;
begin
  if UseTheme then
  begin
    if not Enabled then
      State := ngsDisabled
    else
      State := ngsNormal;

    FThemeEngine.Theme.DrawControlFrame(Canvas, Rect(0, 0, FWidth, FHeight), State);
  end
  else
    inherited ;
end;

procedure TTeThemeEdit.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeEdit.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeEdit.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  if (PopupMenu <> nil) then
    (PopupMenu as TTeThemePopupMenu).ThemeEngine := Value;
    
  Invalidate;
end;

procedure TTeThemeEdit.SetVersion(const Value: TTeThemeVersion);
begin
end;

end.
