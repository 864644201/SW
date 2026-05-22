{==============================================================================

  ThemeEngine's Panel
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemepanels.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemepanels;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,
  te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemePanel class }

{ TTeThemePanel implements a advanced panel control with caption and button. }
  TTeThemePanel = class(TTeCustomPanel)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { inherited }
    function GetCaptionRect: TRect; override;
    procedure DrawCaption; override;
    procedure DrawFace; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    procedure Loaded; override;
  published
    property Align;
    property Blending;
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Transparent;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored False;
  end;

implementation {===============================================================}

{ TTeThemePanel }

constructor TTeThemePanel.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

procedure TTeThemePanel.Loaded;
begin
  inherited Loaded;
{  ThemeEngine := FThemeEngine; }
end;

function TTeThemePanel.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

function TTeThemePanel.GetCaptionRect: TRect;
begin
  Result := Rect(0, 0, Width, Height);

  if ShowBevel then
    InflateRect(Result, -BevelWidth, -BevelWidth);

  Result.Bottom := Result.Top + CaptionHeight;
end;

procedure TTeThemePanel.DrawCaption;
var
  R: TRect;
  State: TTeThemeButtonState;
begin
  if not UseTheme then
    inherited DrawCaption
  else
  begin
    if not ShowCaption then Exit;
    CaptionHeight := FThemeEngine.Theme.GetMetrix(ngmPanelCaptionHeight);

    R := GetCaptionRect;
    FThemeEngine.Theme.DrawPanelCaption(Canvas, R, Caption);

    { Draw Button }
    if not ShowButton then Exit;

    if ButtonPressed then
      State := ngsPressed
    else
      if MouseOnButton then
        State := ngsHot
      else
        State := ngsNormal;

    R := GetButtonRect;
    FThemeEngine.Theme.DrawPanelButton(Canvas, R, ButtonKind, State, Rolled);
  end
end;

procedure TTeThemePanel.DrawFace;
var
  R: TRect;
begin
  if UseTheme then
  begin
    R := Rect(0, 0, Width, Height);
    FThemeEngine.Theme.DrawPanel(Canvas, R, ShowBevel, ShowCaption);
  end
  else
    inherited ;
end;

{ VCL Routines }

procedure TTeThemePanel.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemePanel.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemePanel.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemePanel.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

end.
