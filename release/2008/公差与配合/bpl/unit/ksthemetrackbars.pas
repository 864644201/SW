{==============================================================================

  ThemeEngine's Trackbar
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemetrackbars.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit ksthemetrackbars;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Classes, Controls, Windows, Graphics, Messages, te_controls, KsThemeThemes,
  KsThemeEngine, KsThemeVersion;

type

{ TTeThemeTrackBar }

{ TTeThemeTrackBar is a advanced Windows track bar control. }
  TTeThemeTrackBar = class(TTeCustomTrackBar)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;

    procedure PaintBuffer; override;
    { TrackBar }
    function GetThumbWidth: integer; override;
    procedure DrawTrack(BegCoord, EndCoord, PosCooord: integer); override;
    procedure DrawThumb(APosCoord: integer); override;
    procedure DrawTick(Value, Coord: integer; TopLeft: boolean); override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
  published
    property Blending;
    property Enabled;
    property IntervalHighlightType;
    property LineSize;
    property Max;
    property Min;
    property Orientation;
    property PageSize;
    property Position;
    property Frequency;
    property ShowTicks;
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property ThumbVisible;
    property TickMarks;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
    property OnChange;
  end;

implementation {===============================================================}

uses Extctrls, Dialogs;

{ TTeThemeTrackBar }

constructor TTeThemeTrackBar.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeTrackBar.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeTrackBar.Loaded;
begin
  inherited;
{  ThemeEngine := FThemeEngine; }
end;

{ Theme }

function TTeThemeTrackBar.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ Track Bar }

procedure TTeThemeTrackBar.PaintBuffer;
begin
  if UseTheme and (not Transparent) then
    FillRect(Canvas, Rect(0, 0, Width, Height), FThemeEngine.Theme.GetColor(ngcBtnFace));

  inherited;
end;

function TTeThemeTrackBar.GetThumbWidth: integer;
begin
  if UseTheme then
    Result := FThemeEngine.Theme.GetMetrix(ngmTrackThumbWidth)
  else
    Result := inherited GetThumbWidth;
end;

procedure TTeThemeTrackBar.DrawTrack(BegCoord, EndCoord, PosCooord: integer);
var
  R, LTrackRect, LowIntervalRect, TmpRect: TRect;
  LSideMarign, LTopBottmMarign, LThumbWidth, LThumbHeight: integer;
begin
  if not UseTheme then
    inherited
  else
  begin
    LThumbWidth := GetThumbWidth;
    LTopBottmMarign := GetTopBottomMargin;
    LSideMarign := GetSideMargin;
    LThumbHeight := FThemeEngine.Theme.GetMetrix(ngmTrackThumbHeight);

    with LTrackRect do
    begin
      if Orientation = toHorizontal then
      begin
        Top := LTopBottmMarign;

        Top := Top + (LThumbHeight - FThemeEngine.Theme.GetMetrix(ngmTrackBarHeight)) div 2;

        if ShowTicks and ((TickMarks = tmTopLeft) or (TickMarks = tmBoth)) then
          Top := Top + 4 + 1;

        Bottom := Top + FThemeEngine.Theme.GetMetrix(ngmTrackBarHeight);

        Left := BegCoord;
        Right := EndCoord;

        LowIntervalRect.Top := Top;
        LowIntervalRect.Bottom := Bottom;
        if IntervalHighlightType = htLowInterval then
        begin
          LowIntervalRect.Left := Left;
          LowIntervalRect.Right := PosCooord;
        end
        else
        begin
          LowIntervalRect.Left := PosCooord;
          LowIntervalRect.Right := Right;
        end;
      end
      else
      begin
        Left := LTopBottmMarign;

        Left := Left + (LThumbHeight - FThemeEngine.Theme.GetMetrix(ngmTrackBarHeight)) div 2;

        if ShowTicks and ((TickMarks = tmTopLeft) or (TickMarks = tmBoth)) then
          Left := Left + 4 + 1;

        Right := Left + FThemeEngine.Theme.GetMetrix(ngmTrackBarHeight);

        Top := BegCoord;
        Bottom := EndCoord;

        LowIntervalRect.Left := Left;
        LowIntervalRect.Right := Right;
        if IntervalHighlightType = htLowInterval then
        begin
          LowIntervalRect.Top := Top;
          LowIntervalRect.Bottom := PosCooord;
        end
        else
        begin
          LowIntervalRect.Top := PosCooord;
          LowIntervalRect.Bottom := Bottom;
        end;
      end;
    end;

    { Draw }
    if not Enabled or (IntervalHighlightType = htNone) then
      LowIntervalRect := Rect(0, 0, 0, 0);

    FThemeEngine.Theme.DrawTrackBar(Canvas, LTrackRect, LowIntervalRect);
  end;
end;

procedure TTeThemeTrackBar.DrawThumb(APosCoord: integer);
var
  Bx, By: integer;
  LThumbWidth, LThumbHeight: integer;
  DrawState: TTeThemeButtonState;
begin
  if not ThumbVisible then Exit;

  if not UseTheme then
    inherited
  else
  begin
    LThumbWidth := FThemeEngine.Theme.GetMetrix(ngmTrackThumbWidth);
    LThumbHeight := FThemeEngine.Theme.GetMetrix(ngmTrackThumbHeight);

    if not Enabled then
      DrawState := ngsDisabled
    else
      if ThumbPressed then
        DrawState := ngsPressed
      else
        if MouseOnThumb then
          DrawState := ngsHot
        else
          if Focused then
            DrawState := ngsFocused
          else
            DrawState := ngsNormal;

    if Orientation = toHorizontal then
    begin
      Bx := APosCoord - LThumbWidth div 2;
      By := GetTopBottomMargin;
      if ShowTicks and ((TickMarks = tmTopLeft) or (TickMarks = tmBoth)) then
        By := By + 4 + 1;

      FThemeEngine.Theme.DrawTrackBarThumb(Canvas,
        Rect(Bx, By, Bx + LThumbWidth, By + LThumbHeight), Orientation,
        TickMarks, DrawState);
    end
    else
    begin
      { Orientation = toVertical }

      Bx := GetTopBottomMargin;
      By := APosCoord - LThumbWidth div 2;
      if ShowTicks and ((TickMarks = tmTopLeft) or (TickMarks = tmBoth)) then
        Bx := Bx + 4 + 1;

      FThemeEngine.Theme.DrawTrackBarThumb(Canvas,
        Rect(Bx, By, Bx + LThumbHeight, By + LThumbWidth), Orientation,
        TickMarks, DrawState);
    end;
  end;
end;

procedure TTeThemeTrackBar.DrawTick(Value, Coord: integer;
  TopLeft: boolean);
var
  VCoord: integer;
  LThumbHeight: integer;
  TickColor: TColor;
begin
  if not UseTheme then
    inherited
  else
  begin
    LThumbHeight := FThemeEngine.Theme.GetMetrix(ngmTrackThumbHeight) + 1;

    TickColor := clBtnShadow;

    if TopLeft then
    begin
      VCoord := GetTopBottomMargin + 3;
      if Orientation = toHorizontal then
      begin
        MoveTo(Canvas, Coord, VCoord);
        if (Value = Min) or (Value = Max) then
          LineTo(Canvas, Coord, VCoord - 4, TickColor)
        else
          LineTo(Canvas, Coord, VCoord - 3, TickColor);
      end
      else
      begin
        MoveTo(Canvas, VCoord, Coord);
        if (Value = Min) or (Value = Max) then
          LineTo(Canvas, VCoord - 4, Coord, TickColor)
        else
          LineTo(Canvas, VCoord - 3, Coord, TickColor);
      end;
    end
    else
    begin
      if TickMarks = tmBoth then
        VCoord := GetTopBottomMargin + 4 + 1 +
          LThumbHeight + 1
      else
        VCoord := GetTopBottomMargin + 1 +
          LThumbHeight + 1;

      if Orientation = toHorizontal then
      begin
        MoveTo(Canvas, Coord, VCoord);
        if (Value = Min) or (Value = Max) then
          LineTo(Canvas, Coord, VCoord + 4, TickColor)
        else
          LineTo(Canvas, Coord, VCoord + 3, TickColor);
      end
      else
      begin
        MoveTo(Canvas, VCoord, Coord);
        if (Value = Min) or (Value = Max) then
          LineTo(Canvas, VCoord + 4, Coord, ckBorder)
        else
          LineTo(Canvas, VCoord + 3, Coord, ckBorder);
      end;
    end;
  end;
end;

{ VCL }

procedure TTeThemeTrackBar.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeTrackBar.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeTrackBar.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeTrackBar.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  Invalidate;
end;

end.

