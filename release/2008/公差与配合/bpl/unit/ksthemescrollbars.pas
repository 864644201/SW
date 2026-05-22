{==============================================================================

  ThemeEngine's Scrollbar
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemescrollbars.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemescrollbars;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ExtCtrls, te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeScrollBar class }

{ TTeThemeScrollBar is a advanced Windows scroll bar, which is used to scroll the conTeThements of a window, form, or control. }
  TTeThemeScrollBar = class(TTeCustomScrollBar)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { Overrides }
    procedure PaintBuffer; override;
    { ScrollBar }
    function GetBtnSize: integer; override;
    function GetSliderSize: integer; override;
    procedure DrawLeftTopBtn; override;
    procedure DrawRightBottomBtn; override;
    procedure DrawSlider; override;
    procedure DrawBackground; override;
    procedure DrawBorder; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
  published
    property Blending;
    property ShowButtons;
    property Kind;
    property LargeChange;
    property Max;
    property Min;
    property PageSize;
    property Position;
    property SmallChange;
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
    property OnChange;
    property OnScroll;
  end;

implementation {===============================================================}

{ TTeThemeScrollBar }

constructor TTeThemeScrollBar.Create;
begin
  inherited Create(AOwner);
  BorderWidth := 0;
end;

destructor TTeThemeScrollBar.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeScrollBar.Loaded;
begin
  inherited;
{  ThemeEngine := FThemeEngine; }
end;

function TTeThemeScrollBar.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

function TTeThemeScrollBar.GetBtnSize: integer;
begin
  if UseTheme then
    Result := FThemeEngine.Theme.GetMetrix(ngmSliderWidth)
  else
    Result := inherited GetBtnSize;
end;

function TTeThemeScrollBar.GetSliderSize: integer;
begin
  Result := inherited GetSliderSize;
end;

{ Drawing }

procedure TTeThemeScrollBar.DrawBackground;
begin
  if not UseTheme then
    inherited
  else
  begin
    FThemeEngine.Theme.DrawScrollBar(Canvas, Rect(0, 0, Width, Height), Kind);
  end;
end;

procedure TTeThemeScrollBar.DrawBorder;
begin
  if not UseTheme then
  begin
    inherited ;
    Exit;
  end;
end;

procedure TTeThemeScrollBar.DrawLeftTopBtn;
var
  State: TTeThemeButtonState;
begin
  if not UseTheme then
  begin
    inherited ;
    Exit;
  end;

  if not Enabled then
    State := ngsDisabled
  else
    if LeftTopBtnPressed then
      State := ngsPressed
    else
      if MouseOnLeftTopBtn then
        State := ngsHot
      else
        State := ngsNormal;

  FThemeEngine.Theme.DrawScrollBarButton(Canvas, GetLeftTopBtnRect, Kind, true, State);
end;

procedure TTeThemeScrollBar.DrawRightBottomBtn;
var
  State: TTeThemeButtonState;
begin
  if not UseTheme then
  begin
    inherited ;
    Exit;
  end;

  if not Enabled then
    State := ngsDisabled
  else
    if RightBottomBtnPressed then
      State := ngsPressed
    else
      if MouseOnRightBottomBtn then
        State := ngsHot
      else
        State := ngsNormal;

  FThemeEngine.Theme.DrawScrollBarButton(Canvas, GetRightBottomBtnRect, Kind, false, State);
end;

procedure TTeThemeScrollBar.DrawSlider;
var
  State: TTeThemeButtonState;
begin
  if not UseTheme then
  begin
    inherited ;
    Exit;
  end;

  if not Enabled then
    State := ngsDisabled
  else
    if SliderPressed then
      State := ngsPressed
    else
      if MouseOnSlider then
        State := ngsHot
      else
        State := ngsNormal;

  FThemeEngine.Theme.DrawScrollBarSlider(Canvas, GetSliderRect, Kind, State);
end;

procedure TTeThemeScrollBar.PaintBuffer;
var
  R: TRect;
begin
  if not UseTheme then
  begin
    inherited;
    Exit;
  end;

  inherited;

  DrawBorder;
  DrawBackground;

  if ShowButtons then
  begin
    DrawLeftTopBtn;
    DrawRightBottomBtn;
  end;

  if IsSliderVizible then
  begin
    DrawSlider;

    R := GetLeftTopTrackRect;
    if Kind = sbHorizontal then
      InflateRect(R, 0, -1)
    else
      InflateRect(R, -1, 0);
    if LeftTopTrackPressed then
      FillRect(Canvas, R, clBtnShadow);

    R := GetRightBottomTrackRect;
    if Kind = sbHorizontal then
      InflateRect(R, 0, -1)
    else
      InflateRect(R, -1, 0);
    if RightBottomTrackPressed then
      FillRect(Canvas, R, clBtnShadow);
  end;
end;

procedure TTeThemeScrollBar.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeScrollBar.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeScrollBar.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeScrollBar.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

end.

