{==============================================================================

  ThemeEngine's SpeedButton
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemespeedbuttons.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit ksthemespeedbuttons;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  Menus, Buttons, te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ Use TTeThemeSpeedButton to put a advanced speed button on a form. }
  TTeThemeSpeedButton = class(TTeCustomSpeedButton)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    procedure PaintBuffer; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
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

uses KsThemeSpinButtons;

{ TTeThemeSpeedButton }

constructor TTeThemeSpeedButton.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeSpeedButton.Destroy;
begin
  inherited;
end;

function TTeThemeSpeedButton.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeSpeedButton.PaintBuffer;
var
  DrawState: TTeThemeButtonState;
  ButtonOffset, ChevronOffset: TPoint;
  R: TRect;
begin
  if not UseTheme then
  begin
    inherited ;
    Exit;
  end;

  DrawState := ngsNormal;

  if not Enabled then
    DrawState := ngsDisabled
  else
    if Flat then
    begin
      if MouseInControl then
        DrawState := ngsHot;
      if State = bsDown then
        DrawState := ngsPressed;
    end
    else
    begin
      if State = bsDown then
        DrawState := ngsPressed;
    end;


  if Parent is TTeThemeSpinButton then
  begin
    { Spin button }
    R := GetButtonRect;
    FThemeEngine.Theme.DrawSpinButton(Canvas, R, DrawState, false);
  end
  else
  begin
    { Standard }
    R := GetButtonRect;
    FThemeEngine.Theme.DrawSpeedButton(Canvas, R,
      DrawState, Flat, State = bsExclusive);

    if ShowChevron then
    begin
      R := GetChevronRect;
      FThemeEngine.Theme.DrawSpeedButtonChevron(Canvas, R,
        DrawState, Flat, State = bsExclusive);
    end;
  end;

  { Draw Glyph }
  ButtonOffset := Point(0, 0);
  ChevronOffset := Point(0, 0);

  if FState in [bsDown, bsExclusive] then
    ButtonOffset := Point(1, 1);

  if ShowGlyph then
    DrawGlyph(ButtonOffset);

  if ShowCaption then
    DrawCaption(ButtonOffset);
end;

procedure TTeThemeSpeedButton.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeSpeedButton.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeSpeedButton.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeSpeedButton.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

end.

