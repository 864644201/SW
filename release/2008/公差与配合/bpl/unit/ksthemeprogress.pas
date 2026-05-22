{==============================================================================

  ThemeEngine's Button
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemeprogress.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemeprogress;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Classes, Graphics, Forms, te_controls, KsThemeThemes, KsThemeEngine,
  KsThemeVersion;

type

{ TTeThemeProgressBar class }

{ TTeThemeProgressBar is a advanced Windows progress bar. }
  TTeThemeProgressBar = class(TTeCustomProgressBar)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure PaintBar; override;
    procedure PaintFrame; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    procedure Loaded; override;
  published
    property Blending;
    property BorderWidth;
    property Max;
    property Min;
    property Position;
    property Orientation;
    property Smooth;
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
  end;

implementation {===============================================================}

{ TTeThemeProgressBar }

constructor TTeThemeProgressBar.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

procedure TTeThemeProgressBar.Loaded;
begin
  inherited;
{  ThemeEngine := FThemeEngine; }
end;

function TTeThemeProgressBar.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ Drawing }

procedure TTeThemeProgressBar.PaintBar;
var
  FillR, R: TRect;
  W, Pos: integer;
begin
  if UseTheme then
  begin
    R := GetBarRect;
    if Orientation = kboHorizontal then
      W := RectWidth(R)
    else
      W := RectHeight(R);
    { Calc Pos }
    Pos := Round(W * GetPercent(Position));
    { Set FillR }
    FillR := R;

    if Orientation = kboHorizontal then
      FillR.Right := FillR.Left + Pos
    else
      FillR.Bottom := FillR.Top + Pos;

    { Draw Bar }
    FThemeEngine.Theme.DrawProgessBar(Canvas, R, FillR, Orientation, Smooth);
  end
  else
    inherited;
end;

procedure TTeThemeProgressBar.PaintFrame;
var
  R: TRect;
begin
  if UseTheme then
  begin
    R := Rect(0, 0, FWidth, FHeight);
    { Draw Frame }
    FThemeEngine.Theme.DrawProgessFrame(Canvas, R);
  end
  else
    inherited;
end;

{ VCL }

procedure TTeThemeProgressBar.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

procedure TTeThemeProgressBar.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

function TTeThemeProgressBar.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeProgressBar.SetVersion(const Value: TTeThemeVersion);
begin

end;

end.
