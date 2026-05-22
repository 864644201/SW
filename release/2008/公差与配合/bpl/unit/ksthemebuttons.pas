{==============================================================================

  ThemeEngine's Button
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemebuttons.pas,v 1.1.1.1 2002/08/05 11:50:32 Evgeny Exp $

===============================================================================}

unit ksthemebuttons;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Buttons,
  te_controls, ksthemethemes, ksthemeengine, ksthemeversion;

type

{ TTeThemeButton class }

{ Use TTeThemeButton to put a advanced button on a form. TTeThemeButton introduces several properties to control its behavior in a dialog box setting. Users choose button controls to initiaTeTheme actions. }
  TTeThemeButton = class(TTeCustomButton)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure PaintFace; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    procedure Loaded; override;
  published
    property Align;
    property Blending;
    property BlackAndWhiteGlyph;
    property Default;
    property Enabled;
    property Font;
    property Glyph;
    property Kind;
    property Layout;
    property ModalResult;
    property NumGlyphs;
    property Spacing;
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Transparent;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
    property OnClick;
  end;

implementation {===============================================================}

{ TTeThemeButton }

constructor TTeThemeButton.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

procedure TTeThemeButton.Loaded;
begin
  inherited;
{  ThemeEngine := FThemeEngine; }
end;

function TTeThemeButton.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ Painting }

procedure TTeThemeButton.PaintFace;
var
  R: TRect;
  DrawState: TTeThemeButtonState;
begin
  if UseTheme then
  begin
    R := Rect(0, 0, FWidth, FHeight);

    if not Enabled then
      DrawState := ngsDisabled
    else
      if State = kbsPressed then
        DrawState := ngsPressed
      else
        if MouseInControl then
          DrawState := ngsHot
        else
          if Focused then
            DrawState := ngsFocused
          else
            if ActiveDefault then
              DrawState := ngsDefault
            else
              DrawState := ngsNormal;

    FThemeEngine.Theme.DrawButton(Canvas, FWidth, FHeight, DrawState);
  end
  else
    inherited ;
end;

{ VCL protected }

procedure TTeThemeButton.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeButton.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeButton.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeButton.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

end.
